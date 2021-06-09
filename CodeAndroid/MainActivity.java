package com.example.test_sqlite;

import androidx.appcompat.app.AppCompatActivity;

import android.database.SQLException;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity implements View.OnClickListener, AdapterView.OnItemSelectedListener {

    Controller controller;
    EditText login;
    EditText password;
    EditText way;
    EditText text;
    Spinner spinnerLogin;
    Spinner spinnerWay;
    Spinner spinnerText;
    ArrayAdapter<String> adapterLogin;
    ArrayAdapter<String> adapterWay;
    ArrayAdapter<String> adapterText;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        try {
            controller = new Controller(this);
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }

        Button addLogin = (Button) findViewById(R.id.addUserBtn);
        Button deleteLogin = (Button) findViewById(R.id.deleteUserBtn);
        Button updatePassword = (Button) findViewById(R.id.updateUserBtn);
        Button addWay = (Button) findViewById(R.id.addWayBtn);
        Button deleteWay = (Button) findViewById(R.id.deleteWayBtn);
        Button addText = (Button) findViewById(R.id.addTextBtn);
        Button deleteText = (Button) findViewById(R.id.deleteTextBtn);
        Button updateText = (Button) findViewById(R.id.updateTextBtn);
        addLogin.setOnClickListener(this);
        deleteLogin.setOnClickListener(this);
        updatePassword.setOnClickListener(this);
        addWay.setOnClickListener(this);
        deleteWay.setOnClickListener(this);
        addText.setOnClickListener(this);
        deleteText.setOnClickListener(this);
        updateText.setOnClickListener(this);

        login = (EditText) findViewById(R.id.loginPT);
        password = (EditText) findViewById(R.id.passwordPT);
        way = (EditText) findViewById(R.id.wayPT);
        text = (EditText) findViewById(R.id.textPT);
        spinnerLogin = (Spinner) findViewById(R.id.spinnerLogin);
        spinnerWay = (Spinner) findViewById(R.id.spinnerWay);
        spinnerText = (Spinner) findViewById(R.id.spinnerText);
        spinnerLogin.setOnItemSelectedListener(this);
        spinnerWay.setOnItemSelectedListener(this);

        updateLoginSpinner();
    }

    @Override
    public void onClick(View v) {
        switch (v.getId())
        {
            case R.id.addUserBtn:
            {
                Model.User user = new Model.User();
                user.Login = login.getText().toString();
                user.Password = password.getText().toString();

                controller.AddNewUser(user);
                updateLoginSpinner();
                break;
            }
            case R.id.deleteUserBtn:
            {
                controller.DeleteUserByLogin(login.getText().toString());
                updateLoginSpinner();
                break;
            }
            case R.id.updateUserBtn:
            {
                controller.UpdatePassword(login.getText().toString(), password.getText().toString());
                updateLoginSpinner();
                break;
            }
            case R.id.addWayBtn:
            {

                break;
            }
            case R.id.deleteWayBtn:
            {

                break;
            }
            case R.id.addTextBtn:
            {

                break;
            }
            case R.id.deleteTextBtn:
            {

                break;
            }
            case R.id.updateTextBtn:
            {

                break;
            }
            default:
            {
                break;
            }
        }
    }

    @Override
    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
        switch (parent.getId())
        {
            case R.id.spinnerLogin:
            {
                login.setText((String)parent.getItemAtPosition(position));
                password.setText(controller.GetPasswordByLogin((String)parent.getItemAtPosition(position)));
                updateWaySpinner();
                break;
            }
            case R.id.spinnerWay:
            {
                way.setText((String)parent.getItemAtPosition(position));
                break;
            }
            default:
            {
                break;
            }
        }
    }

    @Override
    public void onNothingSelected(AdapterView<?> parent) {

    }

    public void updateLoginSpinner()
    {
        ArrayList<String> loginList = new ArrayList<String>();
        //login.setText(String.valueOf(loginList.size()));
        loginList = controller.GetAllLogins();

        adapterLogin = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, loginList);
        adapterLogin.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerLogin.setAdapter(adapterLogin);
    }
    public void updateWaySpinner()
    {
        ArrayList<String> wayList = new ArrayList<String>();
        //login.setText(String.valueOf(loginList.size()));
        wayList = controller.GetWaysByLogin(login.getText().toString());

        adapterWay = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, wayList);
        adapterWay.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerWay.setAdapter(adapterWay);
    }
}