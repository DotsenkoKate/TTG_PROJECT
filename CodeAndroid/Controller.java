package com.example.test_sqlite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;

import java.util.ArrayList;
import java.util.List;

public class Controller
{
    static private DBClass DBHelper;
    private SQLiteDatabase ttg_db;
    private Cursor queryReturn;

    public Controller(Context context) throws java.sql.SQLException {
        DBHelper = new DBClass(context);
        DBHelper.create_db();
        ttg_db = DBHelper.open();
    }

    public ArrayList<String> GetAllLogins()
    {
        ArrayList<String> array = new ArrayList<String>();

        queryReturn = ttg_db.rawQuery("SELECT login FROM Users;", null);
        while (queryReturn.moveToNext())
        {
            array.add(queryReturn.getString(0));
        }
        queryReturn.close();

        return array;
    }

    public String GetPasswordByLogin(String login)
    {
        String password;
        queryReturn = ttg_db.rawQuery("SELECT password FROM Users WHERE login = \'" + login + "\';", null);
        queryReturn.moveToNext();
        password = queryReturn.getString(0);
        queryReturn.close();

        return password;
    }


    public void AddNewUser(Model.User user)
    {
        ContentValues cv = new ContentValues();
        cv.put("login", user.Login);
        cv.put("password", user.Password);
        ttg_db.insert("Users", null, cv);
        cv.clear();
    }

    public void DeleteUserByLogin(String login)
    {
        ttg_db.delete("Users", "login='" + login + "'", null);
    }

    public void UpdatePassword(String login, String password)
    {
        ContentValues cv = new ContentValues();
        cv.put("password", password);
        ttg_db.update("Users", cv, "login = '" + login + "';", null);
        cv.clear();
    }

    public ArrayList<String> GetWaysByLogin(String login)
    {
        ArrayList<String> array = new ArrayList<String>();

        queryReturn = ttg_db.rawQuery("SELECT SavedWays.way_number FROM Users JOIN users_ways ON users_ways.user_id = Users.id \n" +
                "JOIN SavedWays ON users_ways.way_number = SavedWays.way_number WHERE login = \'"+ login + "\';", null);
        while (queryReturn.moveToNext())
        {
            array.add(queryReturn.getString(0));
        }
        queryReturn.close();

        return array;
    }
    /*
    public void AddNewWay(String login, Model.Way way)
    {

    }
    public void DeleteWay(String login, int way_number)
    {

    }
    public List<Model.Text> GetTextsByLogin(String login)
    {

    }
    public void AddNewText(String login, Model.Text)
    {

    }
    public void DeleteText(String login, int id)
    {

    }
    public void UpdateText(String login, Model.Text)
    {

    }*/
}
