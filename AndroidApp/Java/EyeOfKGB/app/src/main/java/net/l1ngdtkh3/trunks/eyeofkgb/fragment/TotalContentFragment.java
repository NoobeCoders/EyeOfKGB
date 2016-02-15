package net.l1ngdtkh3.trunks.eyeofkgb.fragment;

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

import net.l1ngdtkh3.trunks.eyeofkgb.LaunchEyeOfKGB;
import net.l1ngdtkh3.trunks.eyeofkgb.model.Repository;
import net.l1ngdtkh3.trunks.eyeofkgb.other.MyAdapter;
import net.l1ngdtkh3.trunks.eyeofkgb.R;

/**
 * Created by Trunks on 22.01.2016.
 */
public class TotalContentFragment extends Fragment {
    public Spinner spinnerSourceUrl;
    public static String TAG = "eyeofkgb";
    public ListView lvTotal;
    public MyAdapter totalAdapter;


    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        final View totalView = inflater.inflate(R.layout.total_content_fragment, null);
        spinnerSourceUrl = (Spinner) totalView.findViewById(R.id.spinner_total_url);
        ArrayAdapter<String> spinnerArrayAdapter = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, LaunchEyeOfKGB.urlList);
        spinnerArrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        //TODO NullPointerException spinnerSourceUrl
        spinnerSourceUrl.setAdapter(spinnerArrayAdapter);
        lvTotal = (ListView) totalView.findViewById(R.id.list_static);
        totalAdapter = new MyAdapter(getActivity(), Repository.getTotalList(spinnerSourceUrl.getSelectedItem().toString()));
        lvTotal.setAdapter(totalAdapter);
        spinnerSourceUrl.setOnItemSelectedListener(
                new AdapterView.OnItemSelectedListener() {
                    public void onItemSelected(
                            AdapterView<?> parent, View view, int position, long id) {
                        Log.v(TAG, "name = " + spinnerSourceUrl.getSelectedItem().toString());
                        totalAdapter.clear();
                        totalAdapter.addAll(Repository.getTotalList(spinnerSourceUrl.getSelectedItem().toString()));
                        totalAdapter.notifyDataSetChanged();
                    }
                    public void onNothingSelected(AdapterView<?> parent) {
                        Log.v(TAG, "parent = " + parent.getResources().toString());
                    }
                });
        return totalView;
    }
}
