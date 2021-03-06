package com.example.trunks.eyeofkgb;

import android.app.DialogFragment;
import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Toast;

import com.example.trunks.eyeofkgb.DB.RealDB;
import com.example.trunks.eyeofkgb.fragment.DailyContentFragment;
import com.example.trunks.eyeofkgb.fragment.TotalContentFragment;
import com.example.trunks.eyeofkgb.model.IDataStatic;
import com.example.trunks.eyeofkgb.model.Repository;
import com.example.trunks.eyeofkgb.model.Something;
import com.example.trunks.eyeofkgb.other.DateDialog;

import java.util.List;


public class ActivityEyeOfKGB extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
    public static String TAG = "eyeofkgb";
    public DrawerLayout drawer;
    public NavigationView navigationView;
    public static Repository repository;
    public static List<String> urlList;
    public static List<String> pearsonList;
    public static List<? extends IDataStatic> newSomethig;
    private static RealDB realDB;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        new MyTask().execute();


        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);


//        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
//        fab.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
//                        .setAction("Action", null).show();
//            }
//        });

        drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);


    }

    private static class MyTask extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            repository = new Repository();
            return null;
        }

        @Override
        protected void onPostExecute(Void aVoid) {
            super.onPostExecute(aVoid);
            urlList = (List<String>) repository.getUrlList();
            pearsonList = (List<String>) repository.getPersonList();
            newSomethig = (List<Something>) repository.getWTF();
        }
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        android.support.v4.app.FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        switch (item.getItemId()) {
            case (R.id.total_static):
                TotalContentFragment totalFR = new TotalContentFragment();
                fragmentTransaction.replace(R.id.home_frame, totalFR);
                fragmentTransaction.commit();
                break;
            case (R.id.daily_static):
                DailyContentFragment dailyFR = new DailyContentFragment();
                fragmentTransaction.replace(R.id.home_frame, dailyFR);
                fragmentTransaction.commit();
                break;
            case (R.id.directory):
                Toast.makeText(getApplicationContext(), "Directory", Toast.LENGTH_SHORT).show();
                break;
            case (R.id.nav_share):
                Toast.makeText(getApplicationContext(), "Share", Toast.LENGTH_SHORT).show();
                break;
            case (R.id.nav_send):
                Toast.makeText(getApplicationContext(), "Send", Toast.LENGTH_SHORT).show();
                break;
            default:
                break;
        }
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    public void setDate(View view) {
        Log.d("TAG", "view id = " + view.getId());
        Bundle bundle = new Bundle();
        DialogFragment newDateFr = new DateDialog();
        switch (view.getId()) {
            case R.id.date_from:
                bundle.putInt(DateDialog.DATE, DateDialog.DATE_FROM);
                newDateFr.setArguments(bundle);
                newDateFr.show(getFragmentManager(), "Date From");
                break;
            case R.id.date_to:
                bundle.putInt(DateDialog.DATE, DateDialog.DATE_TO);
                newDateFr.setArguments(bundle);
                newDateFr.show(getFragmentManager(), "Date to");
                break;
        }


    }
}
