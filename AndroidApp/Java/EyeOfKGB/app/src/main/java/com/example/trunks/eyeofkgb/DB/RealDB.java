package com.example.trunks.eyeofkgb.DB;


import android.util.Log;

import com.example.trunks.eyeofkgb.ActivityEyeOfKGB;
import com.example.trunks.eyeofkgb.model.IDataStatic;
import com.example.trunks.eyeofkgb.model.Something;
import com.example.trunks.eyeofkgb.model.TotalStatic;
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
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;


/**
 * Created by Trunks on 08.02.2016.
 */
public class RealDB implements IGetlist {
    public static String TAG = "eyeofkgb";
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

    public RealDB() {
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

    private ArrayList<CrawlerData> gsonAndHttpUrl() {
        try {
            URL url = new URL(URL_CRAWLER);
            Gson gson = new Gson();
            HttpURLConnection urlC = (HttpURLConnection) url.openConnection();
            urlC.setRequestMethod("GET");
            if (urlC.getResponseCode() == HttpURLConnection.HTTP_OK) {
                StringBuilder sb = new StringBuilder();
                BufferedReader br = new BufferedReader(new InputStreamReader(urlC.getInputStream()));
                String line;
                while ((line = br.readLine()) != null) {
                    sb.append(line);
                }
                br.close();
                urlC.disconnect();
                String jsonResultString = sb.toString();


                ArrayList<CrawlerData> crawlerList = gson.fromJson(jsonResultString, new TypeToken<ArrayList<CrawlerData>>() {
                }.getType());
                if (crawlerList != null) return crawlerList;
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

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
                    if (ts1.getName().equals(cr.getPersonName())) {
                        ts1.setHits(Integer.parseInt(cr.getRankDay()) + ts1.getHits());
                        Log.v(TAG, "cr name  = " + cr.getPersonName() + " cr hits = " + cr.getDataScan());
                    }
                }
            }
        }
        return (List<? extends IDataStatic>) rqBySiteName;
    }
}
