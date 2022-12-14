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
    public partial class CRUDProveedor : Form
    {
        public CRUDProveedor()
        {
            InitializeComponent();
        }

        // Declarar un objeto que instancie el Mapeado Objeto Relacional
        NorthwindDataContext northwind = new NorthwindDataContext();

        private void button2_Click(object sender, EventArgs e)
        {
            var ProveedorEliminado = (from S in northwind.Suppliers
                                      where S.SupplierID == Convert.ToInt32(txtIDProveedor.Text)
                                      select S).First();
            northwind.Suppliers.DeleteOnSubmit(ProveedorEliminado);
            try
            {
                northwind.SubmitChanges(); //Confirmar la transaccion
                dgvProveedor.DataSource = Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se elimino correctamente, error: " + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Suppliers suppliers = northwind.Suppliers.Single(C => C.SupplierID == Convert.ToInt32(txtIDProveedor.Text));
            suppliers.SupplierID = Convert.ToInt32(txtIDProveedor.Text.Trim());
            suppliers.CompanyName = txtNombreEmpresa.Text.Trim();
            suppliers.ContactName = txtNombreContacto.Text.Trim();
            suppliers.Address = txtDireccion.Text.Trim();
            suppliers.City = txtCiudad.Text.Trim();
            suppliers.Region = txtRegion.Text.Trim();
            suppliers.PostalCode = txtCodigoPostal.Text.Trim();
            suppliers.Country = txtContinente.Text.Trim();
            suppliers.Phone = txttxtTelefono.Text.Trim();
            suppliers.Fax = txtFax.Text.Trim();
            try
            {
                northwind.SubmitChanges(); //Confirmar la transaccion
                dgvProveedor.DataSource = Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se actualizo correctamente, error: " + ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private IList<Suppliers> Listar()
        {
            var consulta = from S in northwind.Suppliers
                           select S;

            return consulta.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Suppliers suppliers = new Suppliers();
            //suppliers.SupplierID = Convert.ToInt32(txtIDProveedor.Text.Trim());
            suppliers.CompanyName = txtNombreEmpresa.Text.Trim();
            suppliers.ContactName = txtNombreContacto.Text.Trim();
            suppliers.ContactTitle = txtTitulocontacto.Text.Trim();
            suppliers.Address = txtDireccion.Text.Trim();
            suppliers.City = txtCiudad.Text.Trim();
            suppliers.Region = txtRegion.Text.Trim();
            suppliers.PostalCode = txtCodigoPostal.Text.Trim();
            suppliers.Country = txtContinente.Text.Trim();
            suppliers.Phone = txttxtTelefono.Text.Trim();
            suppliers.Fax = txtFax.Text.Trim();
            suppliers.HomePage = txtPagina.Text.Trim();
            northwind.Suppliers.InsertOnSubmit(suppliers);
            try
            {
                northwind.SubmitChanges(); //Confirmar la transaccion
                dgvProveedor.DataSource = Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se agrego correctamente, error: " + ex);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void proveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formulario = new CRUDProductor();
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
            dgvProveedor.DataSource = consulta;
        }

        private void btnProyeccion2_Click(object sender, EventArgs e)
        {
            var consulta = from S in northwind.Suppliers select new { Codigo = S.SupplierID, Compañia = S.CompanyName, S.City, S.Region, S.Country };
            dgvProveedor.DataSource = consulta;
        }

        private void btnLambda1_Click(object sender, EventArgs e)
        {
            //Productos cuyo precio sea mayor a s/30.00
            var consulta = northwind.Products.Where(P => P.UnitPrice > 30);
            dgvProveedor.DataSource = consulta;
        }

        private void btnLambda2_Click(object sender, EventArgs e)
        {
            //Ordenes que hayan realizados los Empleados con ID 1-5 y hayan sido enviados a USA
            var consulta = northwind.Orders.Where(O => O.EmployeeID <= 5 & O.ShipCountry == "USA");
            dgvProveedor.DataSource = consulta;
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
            dgvProveedor.DataSource = consulta;
        }

        private void Parciales1_Click(object sender, EventArgs e)
        {
            // Nombre completos de los empleados
            var consulta = from E in northwind.Employees
                           select new { E.EmployeeID, E.Title, E.TitleOfCourtesy, FullName = E.FullName() };
            dgvProveedor.DataSource = consulta;
        }

        private void Parciales2_Click(object sender, EventArgs e)
        {
            // 
            var consulta = from C in northwind.Customers
                           select new { C.CustomerID, C.CompanyName, C.ContactName, Origin = C.Origin() };
            dgvProveedor.DataSource = consulta;
        }

        private void Parciales3_Click(object sender, EventArgs e)
        {
            // 
            var consulta = from P in northwind.Products
                           select new { P.ProductID, P.ProductName, P.UnitPrice, P.UnitsInStock, Total = P.Total() };
            dgvProveedor.DataSource = consulta;
        }

        private void Join1_Click(object sender, EventArgs e)
        {
            //
            var consulta = from P in northwind.Products
                           join S in northwind.Suppliers on P.SupplierID equals S.SupplierID
                           join C in northwind.Categories on P.CategoryID equals C.CategoryID
                           select new { P.ProductID, P.ProductName, C.CategoryName, S.CompanyName };
            dgvProveedor.DataSource = consulta;
        }

        private void Join2_Click(object sender, EventArgs e)
        {
            //
            var consulta = from ET in northwind.EmployeeTerritories
                           join E in northwind.Employees on ET.EmployeeID equals E.EmployeeID
                           join T in northwind.Territories on ET.TerritoryID equals T.TerritoryID
                           join R in northwind.Region on T.RegionID equals R.RegionID
                           select new { E.TitleOfCourtesy, FullName = E.FullName(), T.TerritoryDescription, R.RegionDescription };
            dgvProveedor.DataSource = consulta;
        }

        private void Join3_Click(object sender, EventArgs e)
        {
            //
            var consulta = from O in northwind.Orders
                           join C in northwind.Customers on O.CustomerID equals C.CustomerID
                           join S in northwind.Shippers on O.ShipVia equals S.ShipperID
                           join E in northwind.Employees on O.EmployeeID equals E.EmployeeID
                           select new { O.OrderID, Customer = C.ContactName, Employee = E.FullName(), CompanyCustomer = C.CompanyName, CompanyShipper = S.CompanyName };
            dgvProveedor.DataSource = consulta;
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
            dgvProveedor.DataSource = consulta;
        }

        private void bttnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CRUDProveedor_Load(object sender, EventArgs e)
        {
            //Cargar sólo una vez
            dgvProveedor.DataSource = Listar();
        }
    }
}
