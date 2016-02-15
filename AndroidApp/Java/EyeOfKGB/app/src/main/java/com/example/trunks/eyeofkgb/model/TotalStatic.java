package com.example.trunks.eyeofkgb.model;

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
    public String getName() {
        return name;
    }

    @Override
    public void setName(String name) {
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
