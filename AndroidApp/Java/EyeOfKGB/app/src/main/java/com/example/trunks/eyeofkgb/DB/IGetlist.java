package com.example.trunks.eyeofkgb.DB;

import java.util.List;

/**
 * Created by Trunks on 08.02.2016.
 */
public interface IGetlist {
    public List<?> getUrl();

    public List<?> getPersonList();

    public List<?> getWTF();

    public List<?> getTotalListBySite(String siteName);
}
