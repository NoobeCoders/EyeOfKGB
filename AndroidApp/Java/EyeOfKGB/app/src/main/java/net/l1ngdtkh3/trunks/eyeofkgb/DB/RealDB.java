package net.l1ngdtkh3.trunks.eyeofkgb.DB;


import android.support.annotation.NonNull;
import android.util.Log;

import net.l1ngdtkh3.trunks.eyeofkgb.model.DailyStatic;
import net.l1ngdtkh3.trunks.eyeofkgb.model.IDataStatic;
import net.l1ngdtkh3.trunks.eyeofkgb.model.Something;
import net.l1ngdtkh3.trunks.eyeofkgb.model.TotalStatic;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;


import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;


/**
 * Created by Trunks on 08.02.2016.
 */
public class RealDB implements IGetList {
    private static final String ZP = ",";
    private static final String TIRE = "-";
    private static final int KEY_YEAR = 2;
    private static final int KEY_MONTH = 1;
    private static final int KEY_DAY = 0;
    public static String TAG = "eyeofkgb";
    private static final String DT_PERSONS_RANGE = "?dt_persons_range=";
    public static String URL_CRAWLER = "http://crawler.firstexperience.ru/api/v1/rankeveryday/";
    public static final String TAG_RESOURCE_NAME = "site_name";
    public static final String TAG_PERSON_NAME = "person_name";
    public static final String TAG_RANK_DAY = "rank_day";
    public static final String TAG_DATA_SCAN = "data_scan";
    private static RealDB instance;


    private static List<String> listUrl = new ArrayList<>();
    private static List<String> pearsonList = new ArrayList<>();
    private static List<Something> newSomethig = new ArrayList<>();
    private static ArrayList<CrawlerData> crawler;
    private URL url;
    private Gson gson;
    private Thread rqThread;

    public RealDB() {
//        gson = new Gson();
        crawler = gsonAndHttpUrl();
        fillUrlAndPearsonLists();
    }

    private void fillUrlAndPearsonLists() {
        for (CrawlerData cr : crawler) {
            if (!listUrl.contains(cr.getSiteName())) listUrl.add(cr.getSiteName());
            if (!pearsonList.contains(cr.getPersonName())) pearsonList.add(cr.getPersonName());
            Log.v(TAG, "Crawler = " + cr.getSiteName());
        }
    }

    private void openConntec(String Url) {


    }

    @Override
    public List<? extends IDataStatic> getDailyListReq(final String urlrq, String name, String dateFrom, String dateTO) {
        final List<? super IDataStatic> rqByRange = new ArrayList();
        StringBuilder sb = new StringBuilder();
        String[] dateFromSP = getDateToJsonFormat(dateFrom);
        String[] dateToSP = getDateToJsonFormat(dateTO);
        sb.append(URL_CRAWLER).
                append(DT_PERSONS_RANGE).
                append(name).
                append(ZP).
                append(dateFromSP[KEY_YEAR]).
                append(TIRE).
                append(dateFromSP[KEY_MONTH]).
                append(TIRE).
                append(dateFromSP[KEY_DAY]).
                append(ZP).
                append(dateToSP[KEY_YEAR]).
                append(TIRE).
                append(dateToSP[KEY_MONTH]).
                append(TIRE).
                append(dateToSP[KEY_DAY]);
        Log.v(TAG, "here = " + sb.toString());
        final String rqRangeAndname = sb.toString();
        rqThread = new Thread(new Runnable() {
            @Override
            public void run() {
                try {
                    url = new URL(rqRangeAndname);
                    HttpURLConnection urlC = getHttpURLConnection();
                    ArrayList<CrawlerData> crawlerList = null;
                    if (urlC.getResponseCode() == HttpURLConnection.HTTP_OK) {
                        crawlerList = getCrawlerDatas(urlC);
                    }
                    for (CrawlerData r : crawlerList) {
                        Log.v(TAG, "here  " + urlrq);
                        if (r.getSiteName().equals(urlrq)) {
                            Log.v(TAG, "date = " + r.getDataScan() + " hits = " + r.getRankDay() + " name = " + r.getPersonName());
                            rqByRange.add(new DailyStatic(r.getDataScan(), Integer.parseInt(r.getRankDay())));
                        }
                    }
                    Log.v(TAG, " " + urlC.getResponseCode());
                } catch (ProtocolException e) {
                    e.printStackTrace();
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (NullPointerException e) {
                    e.printStackTrace();
                }
            }
        });
        rqThread.start();
        try {
            rqThread.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        Log.v(TAG, "rqByRange = " + rqByRange.size());
        if (rqByRange != null) return (List<? extends IDataStatic>) rqByRange;
        return null;
    }

    @NonNull
    private HttpURLConnection getHttpURLConnection() throws IOException {
        HttpURLConnection urlC = (HttpURLConnection) url.openConnection();
        urlC.setRequestMethod("GET");
        return urlC;
    }

    @NonNull
    private String[] getDateToJsonFormat(String date) {
        return date.split("\\.");
    }

    private ArrayList<CrawlerData> gsonAndHttpUrl() {
        //TODO make method to query and check for responsecode. mb use volley
        try {
            url = new URL(URL_CRAWLER);
            HttpURLConnection urlC = getHttpURLConnection();
            if (urlC.getResponseCode() == HttpURLConnection.HTTP_OK) {
                ArrayList<CrawlerData> crawlerList = getCrawlerDatas(urlC);
                if (crawlerList != null) return crawlerList;
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    private ArrayList<CrawlerData> getCrawlerDatas(HttpURLConnection urlC) throws IOException {
        StringBuilder sb = new StringBuilder();
        BufferedReader br = new BufferedReader(new InputStreamReader(urlC.getInputStream()));
        String line;
        while ((line = br.readLine()) != null) {
            sb.append(line);
        }
        br.close();
        urlC.disconnect();
        String jsonResultString = sb.toString();
        gson = new Gson();
        return gson.fromJson(jsonResultString, new TypeToken<ArrayList<CrawlerData>>() {
        }.getType());
    }

    //bad one
    private void jsonAndUrl() {
        try {
            URL url = new URL(URL_CRAWLER);
            URLConnection urlc = url.openConnection();
            BufferedReader br = new BufferedReader(new InputStreamReader(urlc.getInputStream()));
            String line;
            while ((line = br.readLine()) != null) {
                JSONArray jsonArray = new JSONArray(line);
                for (int i = 0; i < jsonArray.length(); i++) {
                    JSONObject jo = (JSONObject) jsonArray.get(i);
                    Log.v(TAG, "jo = " + jo.getString(TAG_RESOURCE_NAME) + " " + jo.getString(TAG_PERSON_NAME));
                }
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }


    public static synchronized RealDB getInstance() {
        if (instance == null) {
            return instance = new RealDB();
        }
        return instance;
    }

    @Override
    public List<?> getUrl() {
        return listUrl;
    }

    @Override
    public List<?> getPersonList() {
        return pearsonList;
    }

    @Override
    public List<?> getWTF() {
        return newSomethig;
    }

    @Override
    public List<? extends IDataStatic> getTotalListBySite(String siteName) {
        List<? super IDataStatic> rqBySiteName = new ArrayList();
        for (String pearson : pearsonList) {
            rqBySiteName.add(new TotalStatic(pearson, 0));
        }
        for (CrawlerData cr : crawler) {
            if (cr.getSiteName().equals(siteName)) {
                Log.v(TAG, "cr  = " + cr.getSiteName() + " rq sitename = " + siteName);
                for (Object ts : rqBySiteName) {
                    TotalStatic ts1 = (TotalStatic) ts;
                    if (ts1.getDate().equals(cr.getPersonName())) {
                        ts1.setHits(Integer.parseInt(cr.getRankDay()) + ts1.getHits());
                        Log.v(TAG, "cr name  = " + cr.getPersonName() + " cr hits = " + cr.getDataScan());
                    }
                }
            }
        }
        return (List<? extends IDataStatic>) rqBySiteName;
    }
}
