package com.example.trunks.eyeofkgb.model;

/**
 * Created by Trunks on 21.01.2016.
 */
public class Something implements  IDataStatic{
    private String name;
    private int hits;

    public Something(String name, int hits) {
//        super(name, hits);
        this.name = name;
        this.hits = hits;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getHits() {
        return hits;
    }

    public void setHits(int hits) {
        this.hits = hits;
    }
}
