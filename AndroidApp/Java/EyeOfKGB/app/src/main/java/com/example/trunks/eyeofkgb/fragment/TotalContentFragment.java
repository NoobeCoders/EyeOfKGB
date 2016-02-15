package com.example.trunks.eyeofkgb.fragment;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.trunks.eyeofkgb.ActivityEyeOfKGB;
import com.example.trunks.eyeofkgb.model.IDataStatic;
import com.example.trunks.eyeofkgb.model.Repository;
import com.example.trunks.eyeofkgb.model.Something;
import com.example.trunks.eyeofkgb.other.MyAdapter;
import com.example.trunks.eyeofkgb.R;

import java.util.ArrayList;
import java.util.Collection;

/**
 * Created by Trunks on 22.01.2016.
 */
public class TotalContentFragment extends Fragment {
    public Spinner spinnerSourceUrl;
    public static String TAG = "eyeofkgb";
    ListView listStatic;
    MyAdapter totalListAdapter;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        final View totalView = inflater.inflate(R.layout.total_content_fragment, null);
        spinnerSourceUrl = (Spinner) totalView.findViewById(R.id.spinner_total_url);
        ArrayAdapter<String> spinnerArrayAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, ActivityEyeOfKGB.urlList);
        spinnerArrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerSourceUrl.setAdapter(spinnerArrayAdapter);
        listStatic = (ListView) totalView.findViewById(R.id.list_static);
        totalListAdapter = new MyAdapter(getActivity(), ActivityEyeOfKGB.newSomethig);
        listStatic.setAdapter(totalListAdapter);
        spinnerSourceUrl.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
                    public void onItemSelected(
                            AdapterView<?> parent, View view, int position, long id) {
                        Log.v(TAG, "name = " + spinnerSourceUrl.getSelectedItem().toString());
                        totalListAdapter.clear();
                        totalListAdapter.addAll(Repository.getTotalList(spinnerSourceUrl.getSelectedItem().toString()));
                        totalListAdapter.notifyDataSetChanged();
                    }

                    public void onNothingSelected(AdapterView<?> parent) {
                        Log.v(TAG, "parent = " + parent.getResources().toString());
                    }
                });
        return totalView;
    }
}
