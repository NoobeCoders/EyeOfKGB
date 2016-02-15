package net.l1ngdtkh3.trunks.eyeofkgb.fragment;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;

import net.l1ngdtkh3.trunks.eyeofkgb.LaunchEyeOfKGB;
import net.l1ngdtkh3.trunks.eyeofkgb.R;
import net.l1ngdtkh3.trunks.eyeofkgb.model.IDataStatic;
import net.l1ngdtkh3.trunks.eyeofkgb.model.Repository;
import net.l1ngdtkh3.trunks.eyeofkgb.other.MyAdapter;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.List;

/**
 * Created by Trunks on 21.01.2016.
 */
public class DailyContentFragment extends Fragment {
    public static String TAG = "eyeofkgb";
    public Spinner spinnerSourceUrl;
    public Spinner spinnerName;
    public TextView dateFrom;
    public TextView dateTo;
    public Button commitBTN;
    public ListView lvDaily;
    public MyAdapter staticAdapter;
    public TextView dailyStaticSumm;
    public static Calendar cal;

    private SimpleDateFormat sdf;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
//        return super.onCreateView(inflater, container, savedInstanceState);
        View viewDailySt = init(inflater, container);
        sdf = new SimpleDateFormat("dd.MM.yyyy");
        cal = Calendar.getInstance();
        ArrayAdapter<String> urlSpinner = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, LaunchEyeOfKGB.urlList);
        urlSpinner.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        ArrayAdapter<String> nameSpinner = new ArrayAdapter<>(getActivity(), android.R.layout.simple_spinner_item, LaunchEyeOfKGB.pearsonList);
        nameSpinner.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        dateFrom.setText(String.valueOf(sdf.format(cal.getTime())));
        dateTo.setText(String.valueOf(sdf.format(cal.getTime())));
        try {
            spinnerSourceUrl.setAdapter(urlSpinner);
            spinnerName.setAdapter(nameSpinner);
            staticAdapter = new MyAdapter(getActivity(), (List<? extends IDataStatic>) LaunchEyeOfKGB.startTotalList);
            lvDaily.setAdapter(staticAdapter);
        } catch (NullPointerException e) {
            e.printStackTrace();
        }
        commitBTN.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                int summ = 0;
                staticAdapter.clear();
                staticAdapter.addAll(Repository.getDailyListReq(spinnerSourceUrl.getSelectedItem().toString(),
                        spinnerName.getSelectedItem().toString(),
                        dateFrom.getText().toString(),
                        dateTo.getText().toString()));
                for (int i = 0; i < staticAdapter.getCount(); i++) {
                    summ = summ + staticAdapter.getItem(i).getHits();
                    Log.v(TAG, "" + summ);
                }
                staticAdapter.notifyDataSetChanged();
                dailyStaticSumm.setText(String.valueOf(summ));
            }
        });
        return viewDailySt;
    }

    @NonNull
    private View init(LayoutInflater inflater, ViewGroup container) {
        View viewDailySt = inflater.inflate(R.layout.daily_content_fragment, container, false);
        spinnerSourceUrl = (Spinner) viewDailySt.findViewById(R.id.daily_static_url);
        spinnerName = (Spinner) viewDailySt.findViewById(R.id.daily_static_pearson);
        dateFrom = (TextView) viewDailySt.findViewById(R.id.date_from);
        dateTo = (TextView) viewDailySt.findViewById(R.id.date_to);

        commitBTN = (Button) viewDailySt.findViewById(R.id.commitButton);
        lvDaily = (ListView) viewDailySt.findViewById(R.id.daily_list_static);
        dailyStaticSumm = (TextView) viewDailySt.findViewById(R.id.daily_static_summ);
        return viewDailySt;
    }
}
