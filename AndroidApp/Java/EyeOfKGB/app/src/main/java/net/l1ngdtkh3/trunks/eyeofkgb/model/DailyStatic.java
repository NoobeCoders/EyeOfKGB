package net.l1ngdtkh3.trunks.eyeofkgb.model;

/**
 * Created by Trunks on 15.02.2016.
 */
public class DailyStatic implements IDataStatic {
    private String date;
    private int hits;

    public DailyStatic(String date, int hits) {
        this.date = date;
        this.hits = hits;
    }

    @Override
    public String getDate() {
        return date;
    }

    @Override
    public void setDate(String value) {
        date = value;
    }

    @Override
    public int getHits() {
        return hits;
    }

    @Override
    public void setHits(int hits) {
        this.hits = hits;
    }
}
