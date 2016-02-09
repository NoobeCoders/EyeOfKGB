package com.example.trunks.eyeofkgb.other;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.example.trunks.eyeofkgb.ActivityEyeOfKGB;
import com.example.trunks.eyeofkgb.R;
import com.example.trunks.eyeofkgb.model.IDataStatic;
import com.example.trunks.eyeofkgb.model.Something;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Trunks on 21.01.2016.
 */
public class MyAdapter extends ArrayAdapter<IDataStatic> {
    private LayoutInflater inflater = null;
    private Context contex;
    private ArrayList<IDataStatic> list;

    public MyAdapter(Context context, List<? extends IDataStatic> objects) {
        super(context, 0, (List<IDataStatic>) objects);
        this.contex = context;
        this.list = (ArrayList<IDataStatic>) objects;

    }

    @Override
    public int getCount() {
        return list.size();
    }

    @Override
    public IDataStatic getItem(int position) {
        return  list.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        View itemListView = convertView;
        inflater = ((ActivityEyeOfKGB) contex).getLayoutInflater();
        if (itemListView == null) {

            itemListView = inflater.inflate(R.layout.list_item, null);
        }
        TextView name = (TextView) itemListView.findViewById(R.id.name_table_static);
        TextView hits = (TextView) itemListView.findViewById(R.id.hits_table_static);
        IDataStatic someData =  list.get(position);
        name.setText(someData.getName());
        hits.setText(String.valueOf(someData.getHits()));
        return itemListView;
    }

}
