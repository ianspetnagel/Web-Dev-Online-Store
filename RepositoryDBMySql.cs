using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using ProductsAppRPSpetnagel.DataLayer;
using ProductsAppRPSpetnagel.Models;

namespace ProductsAppRPSpetnagel.Repositories
{
    public class RepositoryDBMySql : IProductsRepository
    {
        private IDataAccess _idac = new BridgeDataAccess(new MySqlDataAccess());

        public bool AddProduct(Product pr)
        {
            bool ret = false;

            try
            {
                string sql = "insert into Products(ProductName, Description, Price, StockLevel, CategoryId, PColor, OnSale, Discontinued) values(@ProductName, @Description, @Price, @StockLevel, @CategoryId, @PColor, @OnSale, @Discontinued)";
                List<DbParameter> plist = new List<DbParameter>();

                plist.Add(new MySqlParameter { ParameterName = "@ProductName", MySqlDbType = MySqlDbType.VarChar, Value = pr.ProductName });
                plist.Add(new MySqlParameter { ParameterName = "@Description", MySqlDbType = MySqlDbType.VarChar, Value = pr.Description });
                plist.Add(new MySqlParameter { ParameterName = "@Price", MySqlDbType = MySqlDbType.Decimal, Value = pr.Price });
                plist.Add(new MySqlParameter { ParameterName = "@StockLevel", MySqlDbType = MySqlDbType.Int32, Value = pr.StockLevel });
                plist.Add(new MySqlParameter { ParameterName = "@CategoryId", MySqlDbType = MySqlDbType.Int32, Value = pr.CategoryId });
                plist.Add(new MySqlParameter { ParameterName = "@PColor", MySqlDbType = MySqlDbType.Int32, Value = pr.Pcolor });
                plist.Add(new MySqlParameter { ParameterName = "@OnSale", MySqlDbType = MySqlDbType.Bit, Value = pr.OnSale });
                plist.Add(new MySqlParameter { ParameterName = "@Discontinued", MySqlDbType = MySqlDbType.Bit, Value = pr.Discontinued });

                int rows = _idac.InsertUpdateDelete(sql, plist);

                ret = (rows > 0);
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        public bool ApplyDiscount(int prodid, double amt)
        {
            bool ret = false;

            try
            {
                string sql = "Update Products set Price = Price - Price * @amt/100 where ProductId = @ProductId";
                List<DbParameter> plist = new List<DbParameter>();

                plist.Add(new MySqlParameter { ParameterName = "@ProductId", MySqlDbType = MySqlDbType.Int32, Value = prodid });
                plist.Add(new MySqlParameter { ParameterName = "@amt", MySqlDbType = MySqlDbType.Float, Value = amt });

                int rows = _idac.InsertUpdateDelete(sql, plist);

                ret = (rows > 0);
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        public bool DeleteProduct(int prodid)
        {
            bool ret = false;

            try
            {
                string sql = "Delete from Products where ProductId = @ProductId";
                List<DbParameter> plist = new List<DbParameter>();

                plist.Add(new MySqlParameter { ParameterName = "@ProductId", MySqlDbType = MySqlDbType.Int32, Value = prodid });

                int rows = _idac.InsertUpdateDelete(sql, plist);

                ret = (rows > 0);
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        public List<Category> GetCategories()
        {
            List<Category> CList = new List<Category>();

            try
            {
                string sql = "Select * from Categories";
                DataTable dt = _idac.GetManyRowsCols(sql, null);

                foreach (DataRow dr in dt.Rows)
                {
                    Category cat = new Category
                    {
                        CategoryId = (int)dr["categoryId"],
                        CategoryName = dr["CategoryName"].ToString()
                    };
                    CList.Add(cat);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return CList;
        }

        public Product GetProductById(int prodid)
        {
            Product prod = null;

            try
            {
                string sql = "Select * from Products where ProductId = @ProductId";
                List<DbParameter> plist = new List<DbParameter>();

                plist.Add(new MySqlParameter { ParameterName = "@ProductId", MySqlDbType = MySqlDbType.Int32, Value = prodid });

                DataTable dt = _idac.GetManyRowsCols(sql, plist);

                foreach (DataRow dr in dt.Rows)
                {
                    prod = new Product
                    {
                        ProductId = (int)dr["ProductId"],
                        ProductName = dr["ProductName"].ToString(),
                        Description = dr["Description"].ToString(),
                        Price = (decimal)dr["Price"],
                        Pcolor = dr["PColor"] == DBNull.Value ? null : (int?)dr["PColor"],
                        CategoryId = (int)dr["CategoryId"],
                        StockLevel = (int)dr["StockLevel"],
                        OnSale = (bool)dr["OnSale"],
                        Discontinued = (bool)dr["Discontinued"]
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }

            return prod;
        }

        public List<Product> GetProductsByCategory(int catid)
        {
            List<Product> ProdList = new List<Product>();

            try
            {
                string sql = "Select * from Products where CategoryId = @CategoryId";
                List<DbParameter> plist = new List<DbParameter>();

                plist.Add(new MySqlParameter { ParameterName = "@CategoryId", MySqlDbType = MySqlDbType.Int32, Value = catid });

                DataTable dt = _idac.GetManyRowsCols(sql, plist);

                foreach (DataRow dr in dt.Rows)
                {
                    Product pr = new Product
                    {
                        ProductId = (int)dr["ProductId"],
                        ProductName = dr["ProductName"].ToString(),
                        Description = dr["Description"].ToString(),
                        Price = (decimal)dr["Price"],
                        StockLevel = (int)dr["StockLevel"],
                        OnSale = (bool)dr["OnSale"],
                        Discontinued = (bool)dr["Discontinued"]
                    };
                    ProdList.Add(pr);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ProdList;
        }

        public bool IncreasePrice(int prodid, double amt)
        {
            bool ret = false;

            try
            {
                string sql = "Update Products set Price = Price + Price * @amt/100 where ProductId = @ProductId";
                List<DbParameter> plist = new List<DbParameter>();

                plist.Add(new MySqlParameter { ParameterName = "@ProductId", MySqlDbType = MySqlDbType.Int32, Value = prodid });
                plist.Add(new MySqlParameter { ParameterName = "@amt", MySqlDbType = MySqlDbType.Float, Value = amt });

                int rows = _idac.InsertUpdateDelete(sql, plist);

                ret = (rows > 0);
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }

        public bool UpdateProduct(Product pr)
        {
            bool ret = false;

            try
            {
                string sql = "Update Products set ProductName = @ProductName, Description = @Description, Price = @Price, Description = @Description, StockLevel = @StockLevel, PColor = @PColor, OnSale = @OnSale, Discontinued = @Discontinued where ProductId = @ProductId";

                List<DbParameter> plist = new List<DbParameter>();

                plist.Add(new MySqlParameter { ParameterName = "@ProductId", MySqlDbType = MySqlDbType.Int32, Value = pr.ProductId });
                plist.Add(new MySqlParameter { ParameterName = "@ProductName", MySqlDbType = MySqlDbType.VarChar, Value = pr.ProductName });
                plist.Add(new MySqlParameter { ParameterName = "@Description", MySqlDbType = MySqlDbType.VarChar, Value = pr.Description });
                plist.Add(new MySqlParameter { ParameterName = "@Price", MySqlDbType = MySqlDbType.Decimal, Value = pr.Price });
                plist.Add(new MySqlParameter { ParameterName = "@StockLevel", MySqlDbType = MySqlDbType.Int32, Value = pr.StockLevel });
                plist.Add(new MySqlParameter { ParameterName = "@CategoryId", MySqlDbType = MySqlDbType.Int32, Value = pr.CategoryId });
                plist.Add(new MySqlParameter { ParameterName = "@PColor", MySqlDbType = MySqlDbType.Int32, Value = pr.Pcolor });
                plist.Add(new MySqlParameter { ParameterName = "@OnSale", MySqlDbType = MySqlDbType.Bit, Value = pr.OnSale });
                plist.Add(new MySqlParameter { ParameterName = "@Discontinued", MySqlDbType = MySqlDbType.Bit, Value = pr.Discontinued });

                int rows = _idac.InsertUpdateDelete(sql, plist);

                ret = (rows > 0);
            }
            catch (Exception)
            {
                throw;
            }

            return ret;
        }
    }
}
