using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEditor;

public static class DataBase
{
    static string _connectionPath = "URI=file:" + Application.streamingAssetsPath + "/Arkanoid.db";

    private static IDbConnection _dbconn;

    static DataBase()
    {
        _dbconn = new SqliteConnection(_connectionPath);
    }

    public static IEnumerable<PlayerScore> Loading()
    {
        var data = new List<PlayerScore>();
            
        _dbconn.Open();
        IDbCommand dbcmd = _dbconn.CreateCommand();
        dbcmd.CommandText = "SELECT * " + "FROM Players" + " ORDER BY score DESC";
        IDataReader reader = dbcmd.ExecuteReader();
            
        while (reader.Read())
        {
            var name = reader.GetString(0);
            var score = reader.GetInt32(1);

            data.Add(new PlayerScore(name,score));
        }
            
        reader.Close();
        dbcmd.Dispose();
        _dbconn.Close();

        return data;
    }

    public static void ReplaceAll(IEnumerable<PlayerScore> data)
    {
        _dbconn.Open();
            
        IDbCommand dbcmd = _dbconn.CreateCommand();
        dbcmd.CommandText = "DELETE " + "FROM Players";
        dbcmd.ExecuteNonQuery();

        foreach (var item in data)
        {
            dbcmd.CommandText = $"INSERT INTO Players VALUES (\"{item.name}\",\"{item.score}\")";
            dbcmd.ExecuteNonQuery();
        }
            
        dbcmd.Dispose();
        _dbconn.Close();
    }

    public static void Insert(PlayerScore data)
    {
        _dbconn.Open();
            
        IDbCommand dbcmd = _dbconn.CreateCommand();
        dbcmd.CommandText = $"INSERT INTO Players VALUES (\"{data.name}\",\"{data.score}\")";
        dbcmd.ExecuteNonQuery();
        
        dbcmd.Dispose();
        _dbconn.Close();
    }

    public static void Replaсe(PlayerScore data)
    {
        _dbconn.Open();
            
        IDbCommand dbcmd = _dbconn.CreateCommand();
        dbcmd.CommandText = $"UPDATE Players SET score = {data.score} WHERE name = '{data.name}'";
        dbcmd.ExecuteNonQuery();
        
        dbcmd.Dispose();
        _dbconn.Close();
    }
}

public class PlayerScore
{
    public string name;
    public int score;

    public PlayerScore(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}