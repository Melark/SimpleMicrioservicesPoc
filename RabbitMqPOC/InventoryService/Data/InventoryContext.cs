using Microsoft.EntityFrameworkCore;

namespace InventoryService.Data;

public class InventoryContext: DbContext
{
    public DbSet<InventoryItem> Items { get; set; }
    public DbSet<StockItem> Stock { get; set; }
    public string DbPath { get; }

    // public InventoryContext()
    // {
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     DbPath = System.IO.Path.Join(path, "inventory.db");
    // }
    //
    // // The following configures EF to create a Sqlite database file in the
    // // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");
    
    public InventoryContext (DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }
}


public class InventoryItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    protected InventoryItem()
    {
        
    }

    public InventoryItem(string name)
    {
        Name = name;
    }
}

public class StockItem
{
    public Guid Id { get; set; }
    public InventoryItem Item { get; set; }
    public int QtyAvailable { get; set; }

    protected StockItem()
    {
        
    }

    public StockItem(InventoryItem item, int qtyAvailable)
    {
        Item = item;
        QtyAvailable = qtyAvailable;
    }

    public void Reserve(int amount)
    {
        QtyAvailable -= amount;
    }
}