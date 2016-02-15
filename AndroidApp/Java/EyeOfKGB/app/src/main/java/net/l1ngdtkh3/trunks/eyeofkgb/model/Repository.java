package net.l1ngdtkh3.trunks.eyeofkgb.model;


import net.l1ngdtkh3.trunks.eyeofkgb.DB.RealDB;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Trunks on 25.01.2016.
 */
public class Repository implements Crud  {
    //    private FakeDB fakeDB;
    private static RealDB realDB;

    public Repository() {
        realDB = RealDB.getInstance();
    }

    public static List<? extends IDataStatic> getTotalList(String name) {
        return realDB.getTotalListBySite(name);
    }

    public static List<? extends IDataStatic> getDailyListReq(String url, String name, String dateFrom, String dateTO) {
        return realDB.getDailyListReq(url, name, dateFrom, dateTO);
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
    public List<? extends IDataStatic> getWTF() {
        return new ArrayList<Something>();
    }
}
