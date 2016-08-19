package com.example.trunks.eyeofkgb.DB;

import com.example.trunks.eyeofkgb.model.Something;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Trunks on 25.01.2016.
 */
public class FakeDB implements IGetlist {
    private static FakeDB instance;
    private static List<String> listUrl = new ArrayList<>();
    static private List<String> pearsonList = new ArrayList<>();
    private static List<Something> newSomethig = new ArrayList<>();

    static {
        listUrl.add("lenta.ru");
        listUrl.add("mail.ru");
        listUrl.add("rbc.ru");

        pearsonList.add("putin");
        pearsonList.add("medved");
        pearsonList.add("obama");

        newSomethig.add(new Something("putin", 100555));
        newSomethig.add(new Something("medved", 555));
        newSomethig.add(new Something("obama", 22224));
    }


    protected FakeDB() {
    }

    public static synchronized FakeDB getInstance() {
        if (instance == null) {
            return instance = new FakeDB();
        }
        return instance;
    }


    public List<?> getUrl() {
        return listUrl;
    }

    public List<?> getPersonList() {
        return pearsonList;
    }

    public List<Something> getWTF() {
        return newSomethig;
    }

    @Override
    public List<?> getTotalListBySite(String siteName) {
        return null;
    }
}
