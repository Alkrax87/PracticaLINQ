using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoLINQ
{
    public partial class CRUDProductor : Form
    {
        public CRUDProductor()
        {
            InitializeComponent();
        }

        // Declarar un objeto que instancie el Mapeado Objeto Relacional
        NorthwindDataContext northwind = new NorthwindDataContext();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void CRUDProductor_Load(object sender, EventArgs e)
        {
            //Cargar sólo una vez
            dgvProducto.DataSource = Listar();
        }

        private void proveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formulario = new CRUDProveedor();
            formulario.Show();
            this.Hide();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formulario = new Form1();
            formulario.Show();
            this.Hide();
        }

        private void btnProyeccion1_Click(object sender, EventArgs e)
        {
            var consulta = from P in northwind.Products select new { P.ProductID, P.ProductName, P.UnitPrice };
            dgvProducto.DataSource = consulta;
        }

        private void btnProyeccion2_Click(object sender, EventArgs e)
        {
            var consulta = from S in northwind.Suppliers select new { Codigo = S.SupplierID, Compañia = S.CompanyName, S.City, S.Region, S.Country };
            dgvProducto.DataSource = consulta;
        }

        private void btnLambda1_Click(object sender, EventArgs e)
        {
            //Productos cuyo precio sea mayor a s/30.00
            var consulta = northwind.Products.Where(P => P.UnitPrice > 30);
            dgvProducto.DataSource = consulta;
        }

        private void btnLambda2_Click(object sender, EventArgs e)
        {
            //Ordenes que hayan realizados los Empleados con ID 1-5 y hayan sido enviados a USA
            var consulta = northwind.Orders.Where(O => O.EmployeeID <= 5 & O.ShipCountry == "USA");
            dgvProducto.DataSource = consulta;
        }

        private void btnLambda3_Click(object sender, EventArgs e)
        {
            //Detalles de orden donde el costo de unidad sea mayor a 20, el descuento mayor a 25%, la cantidad mayor a 20 y muestre el nombre del producto
            var consulta = from OD in northwind.Order_Details.Where(OD => OD.UnitPrice >= 20 & OD.Discount >= 0.25 & OD.Quantity > 20)
                           join P in northwind.Products on OD.ProductID equals P.ProductID
                           select new
                           {
                               P.ProductName,
                               OD.UnitPrice,
                               OD.Discount,
                               OD.Quantity
                           };
            dgvProducto.DataSource = consulta;
        }

        private void Parciales1_Click(object sender, EventArgs e)
        {
            // Nombre completos de los empleados
            var consulta = from E in northwind.Employees
                           select new { E.EmployeeID, E.Title, E.TitleOfCourtesy, FullName = E.FullName() };
            dgvProducto.DataSource = consulta;
        }

        private void Parciales2_Click(object sender, EventArgs e)
        {
            // Nombre completos de los empleados
            var consulta = from C in northwind.Customers
                           select new { C.CustomerID, C.CompanyName, C.ContactName, Origin = C.Origin() };
            dgvProducto.DataSource = consulta;
        }

        private void Parciales3_Click(object sender, EventArgs e)
        {
            // Nombre completos de los empleados
            var consulta = from P in northwind.Products
                           select new { P.ProductID, P.ProductName, P.UnitPrice, P.UnitsInStock, Total = P.Total() };
            dgvProducto.DataSource = consulta;
        }

        private void Join1_Click(object sender, EventArgs e)
        {
            //
            var consulta = from P in northwind.Products
                           join S in northwind.Suppliers on P.SupplierID equals S.SupplierID
                           join C in northwind.Categories on P.CategoryID equals C.CategoryID
                           select new { P.ProductID, P.ProductName, C.CategoryName, S.CompanyName };
            dgvProducto.DataSource = consulta;
        }

        private void Join2_Click(object sender, EventArgs e)
        {
            //
            var consulta = from ET in northwind.EmployeeTerritories
                           join E in northwind.Employees on ET.EmployeeID equals E.EmployeeID
                           join T in northwind.Territories on ET.TerritoryID equals T.TerritoryID
                           join R in northwind.Region on T.RegionID equals R.RegionID
                           select new { E.TitleOfCourtesy, FullName = E.FullName(), T.TerritoryDescription, R.RegionDescription };
            dgvProducto.DataSource = consulta;
        }

        private void Join3_Click(object sender, EventArgs e)
        {
            //
            var consulta = from O in northwind.Orders
                           join C in northwind.Customers on O.CustomerID equals C.CustomerID
                           join S in northwind.Shippers on O.ShipVia equals S.ShipperID
                           join E in northwind.Employees on O.EmployeeID equals E.EmployeeID
                           select new { O.OrderID, Customer = C.ContactName, Employee = E.FullName(), CompanyCustomer = C.CompanyName, CompanyShipper = S.CompanyName };
            dgvProducto.DataSource = consulta;
        }

        private void Join4_Click(object sender, EventArgs e)
        {
            // Qué productos hizo pedido el cliente y en qué fechas (ordenado por fechas)
            var consulta = from OD in northwind.Order_Details
                           join O in northwind.Orders on OD.OrderID equals O.OrderID
                           join C in northwind.Customers on O.CustomerID equals C.CustomerID
                           join P in northwind.Products on OD.ProductID equals P.ProductID
                           orderby O.OrderDate ascending
                           select new { C.CustomerID, C.ContactName, P.ProductName, O.OrderDate };
            dgvProducto.DataSource = consulta;
        }

        private void bttnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private IList<Products> Listar()
        {
            var consulta = from P in northwind.Products
                           select P;

            return consulta.ToList();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            //products.ProductID = Convert.ToInt32(txtIdProducto.Text.Trim());
            products.ProductName = txtNombreProducto.Text.Trim();
            products.SupplierID = Convert.ToInt32(txtIdProveedor.Text.Trim());
            products.CategoryID = Convert.ToInt32(txtIdCategoria.Text.Trim());
            products.QuantityPerUnit = txtPrecioUnitario.Text.Trim();
            products.UnitPrice = Convert.ToInt32(txtPrecioUnitario.Text.Trim());
            products.UnitsInStock = (short?)Convert.ToInt32(txtUnidadesenStock.Text.Trim());
            products.UnitsOnOrder = (short?)Convert.ToInt32(txtUnidadesPedido.Text.Trim());
            products.ReorderLevel = (short?)Convert.ToInt32(txtReordenarNivel.Text.Trim());
            //products.Discontinued = Convert.ToBoolean(txtDiscontinued.Text.Trim());
            northwind.Products.InsertOnSubmit(products);
            try
            {
                northwind.SubmitChanges(); //Confirmar la transaccion
                dgvProducto.DataSource = Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se agrego correctamente, error: " + ex);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Products products = northwind.Products.Single(C => C.ProductID == Convert.ToInt32(txtIdProducto.Text));
            products.ProductID = Convert.ToInt32(txtIdProducto.Text.Trim());
            products.ProductName = txtNombreProducto.Text.Trim();
            products.SupplierID = Convert.ToInt32(txtIdProveedor.Text.Trim());
            products.CategoryID = Convert.ToInt32(txtIdCategoria.Text.Trim());
            products.QuantityPerUnit = txtPrecioUnitario.Text.Trim();
            products.UnitPrice = Convert.ToInt32(txtPrecioUnitario.Text.Trim());
            products.UnitsInStock = (short?)Convert.ToInt32(txtUnidadesenStock.Text.Trim());
            products.UnitsOnOrder = (short?)Convert.ToInt32(txtUnidadesPedido.Text.Trim());
            products.ReorderLevel = (short?)Convert.ToInt32(txtReordenarNivel.Text.Trim());
            try
            {
                northwind.SubmitChanges(); //Confirmar la transaccion
                dgvProducto.DataSource = Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se actualizo correctamente, error: " + ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var ProductoEliminado = (from P in northwind.Products
                                     where P.ProductID == Convert.ToInt32(txtIdProducto.Text)
                                     select P).First();
            northwind.Products.DeleteOnSubmit(ProductoEliminado);
            try
            {
                northwind.SubmitChanges(); //Confirmar la transaccion
                dgvProducto.DataSource = Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se elimino correctamente, error: " + ex);
            }
        }
    }
}
