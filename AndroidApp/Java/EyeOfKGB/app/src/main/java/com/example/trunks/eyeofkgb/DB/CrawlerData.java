package com.example.trunks.eyeofkgb.DB;

import com.google.gson.annotations.SerializedName;


/**
 * Created by Trunks on 09.02.2016.
 */
public class CrawlerData {

    public String getSiteName() {
        return siteName;
    }

    public void setSiteName(String siteName) {
        this.siteName = siteName;
    }

    public String getPersonName() {
        return personName;
    }

    public void setPersonName(String personName) {
        this.personName = personName;
    }

    public String getRankDay() {
        return rankDay;
    }

    public void setRankDay(String rankDay) {
        this.rankDay = rankDay;
    }

    public String getDataScan() {
        return dataScan;
    }

    public void setDataScan(String dataScan) {
        this.dataScan = dataScan;
    }

    @SerializedName(RealDB.TAG_RESOURCE_NAME)
    public String siteName;

    @SerializedName(RealDB.TAG_PERSON_NAME)
    public String personName;

    @SerializedName(RealDB.TAG_RANK_DAY)
    public String rankDay;

    @SerializedName(RealDB.TAG_DATA_SCAN)
    public String dataScan;

}
