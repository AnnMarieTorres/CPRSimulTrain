// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: CPRSimulTrain DB
// Vers: 3.0.0.0
//
// Low-level SQLite routine(s)
// .............................................................
using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

namespace CPRSimulTrain
{

  public class CPRSimulTrainDB
  {
    readonly SQLiteAsyncConnection database;


    // ======================================================================
    // Create DB
    // Create Table
    //
    // Called from 'CPRSimulTrainDB Database' in App.cs
    // And only if a new DB must be created. Only once per run if at all.
    // ======================================================================
    public CPRSimulTrainDB(string dbPath)
    {
      database = new SQLiteAsyncConnection(dbPath);
      database.CreateTableAsync<itemsDBTable>().Wait();
    }

    // ======================================================================
    // List ALL data
    //
    // Read all data, and store in the 'listView.ItemsSource' construct
    // via the 'itemsDBTable' class (itemsDBTable.cs)
    // ======================================================================
    public Task<List<itemsDBTable>> GetItemsAsync()
    {
      return database.Table<itemsDBTable>().ToListAsync();
    }

    // ======================================================================
    // Get Item under certain conditions
    //
    // ======================================================================
    public Task<List<itemsDBTable>> GetItemsNotDoneAsync()
    {
      return database.QueryAsync<itemsDBTable>("SELECT * FROM [itemTable] WHERE [RuleNameDB] = 0");
    }

    // ======================================================================
    // Get item under certain 'queries'
    //
    // ======================================================================
    public Task<itemsDBTable> GetItemAsync(int id)
    {
      return database.Table<itemsDBTable>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    // ======================================================================
    // Save:
    // (1) update if record exists
    // (2) insert if a new record
    //
    // The record construct is via the 'itemsDBTable' class (itemsDBTable.cs)
    // ======================================================================
    public Task<int> SaveItemAsync(itemsDBTable item)
    {
      if (item.ID != 0)
      {
        return database.UpdateAsync(item);
      }
      else
      {
        return database.InsertAsync(item);
      }
    }


    // ======================================================================
    // SaveSpecial:
    // (1) try update
    // (2) insert if a new record
    //
    // The record construct is via the 'itemsDBTable' class (itemsDBTable.cs)
    // ======================================================================
    public Task<int> SaveSpecialItemAsync(itemsDBTable item)
    {
        if (item.ID != 0)
        {
          return database.UpdateAsync(item);
        }
        else
        {
          return database.InsertAsync(item);
        }
    }


    // ======================================================================
    // Delete a record
    //
    // ======================================================================
    public Task<int> DeleteItemAsync(itemsDBTable item)
    {
      return database.DeleteAsync(item);
    }
  }
}

