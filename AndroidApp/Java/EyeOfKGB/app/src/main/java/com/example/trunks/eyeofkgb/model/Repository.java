package com.example.trunks.eyeofkgb.model;


import com.example.trunks.eyeofkgb.DB.FakeDB;
import com.example.trunks.eyeofkgb.DB.RealDB;

import java.util.List;

/**
 * Created by Trunks on 25.01.2016.
 */
public class Repository implements Crud {
    private FakeDB fakeDB;
    private static RealDB realDB;

    public Repository() {
        realDB = RealDB.getInstance();
        fakeDB = FakeDB.getInstance();
    }

    public static List<? extends IDataStatic> getTotalList(String name) {
        return realDB.getTotalListBySite(name);
    }

    @Override
    public List<?> getUrlList() {
        return realDB.getUrl();
    }

    @Override
    public List<?> getPersonList() {
        return realDB.getPersonList();
    }

    @Override
    public List<Something> getWTF() {
        return fakeDB.getWTF();
    }
}
