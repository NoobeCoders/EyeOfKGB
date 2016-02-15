package net.l1ngdtkh3.trunks.eyeofkgb.DB;

import java.util.List;

/**
 * Created by Trunks on 08.02.2016.
 */
public interface IGetList {
    public List<?> getUrl();

    public List<?> getPersonList();

    public List<?> getWTF();

    public List<?> getTotalListBySite(String siteName);

    public List<?> getDailyListReq(String url, String name, String dateFrom, String dateTO);
}
