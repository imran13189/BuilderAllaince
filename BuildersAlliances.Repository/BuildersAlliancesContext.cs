using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using BuildersAlliances.Domain;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace BuildersAlliances.Repository
{
   public class BuildersAlliancesContext: DbContext
    {
        public BuildersAlliancesContext()
            : base("name=BuildersAlliancesDBContext")
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public System.Data.Entity.DbSet<Users> Users { get; set; }
        public System.Data.Entity.DbSet<Manufacturer> Manufacturer { get; set; }
        public System.Data.Entity.DbSet<Log> Log { get; set; }
        public System.Data.Entity.DbSet<LogType> LogType { get; set; }
        public System.Data.Entity.DbSet<LogProperty> LogProperty { get; set; }
        public System.Data.Entity.DbSet<LogPropertyChange> LogPropertyChange { get; set; }
        public System.Data.Entity.DbSet<LogInfo> LogInfoes { get; set; }
        public System.Data.Entity.DbSet<Items> Items { get; set; }
        public System.Data.Entity.DbSet<Inventory> Inventory { get; set; }
        public System.Data.Entity.DbSet<OrderType> OrderType { get; set; }
        public System.Data.Entity.DbSet<Orders> Orders { get; set; }
        public System.Data.Entity.DbSet<Trucks> Trucks { get; set; }

        public System.Data.Entity.DbSet<DiscountType> DiscountType { get; set; }

        public System.Data.Entity.DbSet<OrderItem> OrderItem { get; set; }

        public System.Data.Entity.DbSet<TruckType> TruckType { get; set; }
        public System.Data.Entity.DbSet<ItemDiscounts> ItemDiscounts { get; set; }
        public System.Data.Entity.DbSet<OrderItemStatus> OrderItemStatus { get; set; }
        public System.Data.Entity.DbSet<Colors> Colors { get; set; }
        public System.Data.Entity.DbSet<DoorStyle> DoorStyle { get; set; }
        public System.Data.Entity.DbSet<OrderStatus> OrderStatus { get; set; }
        public System.Data.Entity.DbSet<Notification> Notification { get; set; }
        public System.Data.Entity.DbSet<Builder> Builder { get; set; }
        public System.Data.Entity.DbSet<Roles> Roles { get; set; }
        public System.Data.Entity.DbSet<UserInRole> UserInRole { get; set; }
        public System.Data.Entity.DbSet<Qoute> Qoute { get; set; }
        public System.Data.Entity.DbSet<QouteItems> QouteItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscountType>().Property(x => x.Multiplier).HasPrecision(16, 3);
            modelBuilder.Entity<ItemDiscounts>().Property(x => x.Multiplier).HasPrecision(16, 3);


        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            var context = ((IObjectContextAdapter)(this)).ObjectContext;

            var connection = this.Database.Connection;
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                //command to execute
                cmd.CommandText = commandText;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;

                if (parameters != null)
                    foreach (var p in parameters)
                    {
                        if (p != null)
                            cmd.Parameters.Add(p);
                    }
                IList<TEntity> result;

                using (var reader = cmd.ExecuteReader())
                {
                    result = context.Translate<TEntity>(reader).ToList();
                    for (int i = 0; i < result.Count; i++)
                        result[i] = AttachEntityToContext(result[i]);
                }


                return result;
            }
        }
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            //var alreadyAttached = Set<TEntity>().Local.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (entity.Id > 0)
            {
                var alreadyAttached = Set<TEntity>().Local.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (alreadyAttached == null)
                {
                    //attach new entity
                    Set<TEntity>().Attach(entity);
                    return entity;
                }
                else
                {
                    //entity is already loaded.
                    return alreadyAttached;
                }
            }
            else
            {
                return entity;
            }
        }
        public static bool GetLogInfoStatus(string ModuleName)
        {
            bool returnVal = false;
            var conn = new SqlConnection();
            var sCmd = new SqlCommand
            {
                CommandText = "GetLogs",
                CommandTimeout = 600,
                CommandType = CommandType.StoredProcedure
            };
            sCmd.Parameters.AddWithValue("ModuleName", ModuleName);
            SqlDataReader drResults = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    sCmd.Connection = conn;
                    drResults = sCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    sCmd.Dispose();

                    if (drResults.HasRows)
                    {
                        while (drResults.Read())
                        {
                            int rowsAffected = (drResults["RowsAffected"] != DBNull.Value) ? int.Parse(drResults["RowsAffected"].ToString()) : 0;
                            if (rowsAffected > 0)
                                returnVal = true;
                        }

                        drResults.Close();
                        drResults.Dispose();
                    }
                    drResults = null;
                }
                else
                {
                    throw new Exception("Database connection could not be established.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading Recurly GetLogs from [Framework].[dbo].[GetLogs].", ex);
            }
            finally
            {
                if (drResults != null && !drResults.IsClosed)
                {
                    drResults.Close();
                    drResults.Dispose();
                }

                if (conn.State != ConnectionState.Closed)
                    conn.Close();

                conn.Dispose();
            }
            return returnVal;
        }

       
    }
}
