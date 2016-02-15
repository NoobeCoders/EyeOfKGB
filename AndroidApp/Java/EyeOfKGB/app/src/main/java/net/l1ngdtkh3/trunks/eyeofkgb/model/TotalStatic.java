package net.l1ngdtkh3.trunks.eyeofkgb.model;

/**
 * Created by Trunks on 09.02.2016.
 */
public class TotalStatic implements IDataStatic {
    private String name;
    private int hits;

    public TotalStatic(String name, int hits) {
        this.name = name;
        this.hits = hits;
    }

    @Override
    public String getDate() {
        return name;
    }

    @Override
    public void setDate(String name) {
        this.name = name;
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
