package net.l1ngdtkh3.trunks.eyeofkgb.other;

import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.DialogFragment;
import android.os.Bundle;
import android.widget.DatePicker;
import android.widget.TextView;

import net.l1ngdtkh3.trunks.eyeofkgb.R;

import java.util.Calendar;

/**
 * Created by Trunks on 22.01.2016.
 */
public class DateDialog extends DialogFragment implements DatePickerDialog.OnDateSetListener {
    public static final String DATE = "DATE";
    public final static int DATE_FROM = 1;
    public final static int DATE_TO = 2;
    private int year;
    private int month;
    private int day;
    private TextView tv;
    private int tmp;


    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        final Calendar c = Calendar.getInstance();
        year = c.get(Calendar.YEAR);
        month = c.get(Calendar.MONTH);
        day = c.get(Calendar.DAY_OF_MONTH);
        Bundle bundle = this.getArguments();
        tmp = bundle.getInt(DATE);
        DatePickerDialog dpd = new DatePickerDialog(getActivity(), AlertDialog.THEME_HOLO_DARK, this, year, month, day);
        DatePicker dp = dpd.getDatePicker();
        dp.setMaxDate(c.getTimeInMillis());
        return dpd;
    }

    @Override
    public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
        if (tmp == 1) {
            tv = (TextView) getActivity().findViewById(R.id.date_from);
        } else if (tmp == 2) {
            tv = (TextView) getActivity().findViewById(R.id.date_to);
        }

        tv.setText(getDateFix(dayOfMonth) + dayOfMonth + "." + getDateFix(monthOfYear) + (monthOfYear + 1) + "." + year);
        //TODO fix date
    }

    public static String getDateFix(int date) {
        return date < 10 ? "0" : "";
    }
}
