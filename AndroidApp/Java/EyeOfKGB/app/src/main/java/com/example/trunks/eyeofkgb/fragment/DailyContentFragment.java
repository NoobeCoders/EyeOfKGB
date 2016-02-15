package com.example.trunks.eyeofkgb.fragment;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

import com.example.trunks.eyeofkgb.ActivityEyeOfKGB;
import com.example.trunks.eyeofkgb.R;

/**
 * Created by Trunks on 21.01.2016.
 */
public class DailyContentFragment extends Fragment {
    Spinner spinnerSourceUrl;
    Spinner spinnerName;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
//        return super.onCreateView(inflater, container, savedInstanceState);
        View viewDailySt = inflater.inflate(R.layout.daily_content_fragment, container, false);
        spinnerSourceUrl = (Spinner) viewDailySt.findViewById(R.id.daily_static_url);
        spinnerName = (Spinner) viewDailySt.findViewById(R.id.daily_static_pearson);
        ArrayAdapter<String> spinnerArrayAdapter1 = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, ActivityEyeOfKGB.urlList);
        spinnerArrayAdapter1.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerSourceUrl.setAdapter(spinnerArrayAdapter1);

        ArrayAdapter<String> spinnerArrayAdapter2 = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, ActivityEyeOfKGB.pearsonList);
        spinnerArrayAdapter2.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerName.setAdapter(spinnerArrayAdapter2);

        return viewDailySt;
    }
}
