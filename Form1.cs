using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        private void dateTimePicker1_MouseCaptureChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'vacDataSet1.VACAC' Puede moverla o quitarla según sea necesario.
            this.vACACTableAdapter1.Fill(this.vacDataSet1.VACAC);
            // TODO: esta línea de código carga datos en la tabla 'vacDataSet.VACAC' Puede moverla o quitarla según sea necesario.
            this.vACACTableAdapter.Fill(this.vacDataSet.VACAC);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");
            conexion.Open();
            //textBox12.Text= comboBox1.SelectedValue.ToString();
            //string cod = comboBox1.;
            string cadena = "SELECT  ci,estado,ciudad,centroCosto,Posicion,FechaIngreso,FechaSalida,AntiuedadMeses,TotalDiasAcumulados,DiasTomados,SaldoDiasPendiente FROM VACAC  where ci=" + comboBox1.SelectedValue.ToString();
            //string cadena = "SELECT  ci,SaldoDiasPendiente FROM VACAC  where ci=" + comboBox1.SelectedValue.ToString();

            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                cedulatxt.Text = registro["ci"].ToString();
                estadotxt.Text = registro["estado"].ToString();
                ciudadtxt.Text = registro["ciudad"].ToString();
                centrocostotxt.Text = registro["centroCosto"].ToString();
                posiciontxt.Text = registro["Posicion"].ToString();
                fingresotxt.Text = registro["FechaIngreso"].ToString();
                fsalidatxt.Text = registro["FechaSalida"].ToString();
                antiguedadtxt.Text = registro["AntiuedadMeses"].ToString();
                diasacumuladostxt.Text = registro["TotalDiasAcumulados"].ToString();
                diastomadostxt.Text = registro["DiasTomados"].ToString();
                diaspendientestxt.Text = registro["SaldoDiasPendiente"].ToString();             
            }
            else
                MessageBox.Show("No existe un artículo con el código ingresado");
            conexion.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime date_1 = Convert.ToDateTime(dateTimePicker1.Value.ToString("dd/MM/yyyy"));
            DateTime date_2 = Convert.ToDateTime(fingresotxt.Text.ToString());

            //DateTime Diff_dates = date_1.AddMonths(date_2);
            antiguedadcal.Text=((date_1 - date_2).TotalDays/30).ToString();
            double diasacum = (Convert.ToDouble(antiguedadcal.Text))*(1.25);
            diasacum = (double)decimal.Round((decimal)diasacum, 2);
            diasacumuladoscal.Text=diasacum.ToString();
            double vac =diasacum - Convert.ToDouble(diastomadostxt.Text);
            vac=(double)decimal.Round((decimal)vac, 2);
            diaspendientescal.Text=vac.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");
            
            string estado = cbactualizarestado.Text;
            string ced = textBox17.Text;
            //string cadena = "UPDATE VACAC SET estado='"+estado+ "' WHERE  ci="+ced ;
            conexion.Open();

            string query = "UPDATE VACAC SET estado = @estadoEmp WHERE CI = @ced";

            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@estadoEmp", estado);

                // Asegúrate de asignar el valor de CI desde el TextBox como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ced", ced);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
                MessageBox.Show("Estado actualizado de: "+ comboBox2.Text, "Usuario Actualizado correctamente", MessageBoxButtons.OK);


            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           textBox17.Text = comboBox2.SelectedValue.ToString();

            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");
            conexion.Open();
            //textBox12.Text= comboBox1.SelectedValue.ToString();
            //string cod = comboBox1.;
            string cadena = "SELECT  ci,estado,ciudad,centroCosto,Posicion,FechaIngreso,FechaSalida,AntiuedadMeses,TotalDiasAcumulados,DiasTomados,SaldoDiasPendiente FROM VACAC  where ci=" + comboBox1.SelectedValue.ToString();
            //string cadena = "SELECT  ci,SaldoDiasPendiente FROM VACAC  where ci=" + comboBox1.SelectedValue.ToString();

            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
               /*cedulatxt.Text = registro["ci"].ToString();
                estadotxt.Text = registro["estado"].ToString();
                ciudadtxt.Text = registro["ciudad"].ToString();
                centrocostotxt.Text = registro["centroCosto"].ToString();
                posiciontxt.Text = registro["Posicion"].ToString();
                fingresotxt.Text = registro["FechaIngreso"].ToString();
                fsalidatxt.Text = registro["FechaSalida"].ToString();
                antiguedadtxt.Text = registro["AntiuedadMeses"].ToString();
                diasacumuladostxt.Text = registro["TotalDiasAcumulados"].ToString();
                diastomadostxt.Text = registro["DiasTomados"].ToString();
                diaspendientestxt.Text = registro["SaldoDiasPendiente"].ToString();*/
                txtantiguedadac.Text= registro["AntiuedadMeses"].ToString();
                txtfingresoac.Text= registro["FechaIngreso"].ToString();
                txtdiasacumuladosac.Text = registro["TotalDiasAcumulados"].ToString();
                txtdiastomadosac.Text = registro["DiasTomados"].ToString();
                txtdiaspendientesac.Text = registro["SaldoDiasPendiente"].ToString();
                
            }
            else
                MessageBox.Show("No existe un artículo con el código ingresado");
            conexion.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");

            string ciudad = txtnuevaciudad.Text;
            string ced = textBox17.Text;
            //string cadena = "UPDATE VACAC SET ciudad='"+estado+ "' WHERE  ci="+ced ;
            conexion.Open();

            string query = "UPDATE VACAC SET ciudad = @ciudadnue WHERE CI = @ced";

            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ciudadnue", ciudad);

                // Asegúrate de asignar el valor de CI desde el TextBox como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ced", ced);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
                MessageBox.Show("Ciudad actualizada de: " + comboBox2.Text, "Ciudad Actualizada correctamente", MessageBoxButtons.OK);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");

            string ccosto = txtnuevocentrodecosto.Text;
            string ced = textBox17.Text;
            //string cadena = "UPDATE VACAC SET ciudad='"+estado+ "' WHERE  ci="+ced ;
            conexion.Open();

            string query = "UPDATE VACAC SET centroCosto = @ccosto WHERE CI = @ced";

            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ccosto", ccosto);

                // Asegúrate de asignar el valor de CI desde el TextBox como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ced", ced);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
                MessageBox.Show("Centro de Costo actualizado de: " + comboBox2.Text, "Centro de costo Actualizado correctamente", MessageBoxButtons.OK);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime fechasalida = dtfechasalidaac.Value;
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");

            //string ccosto = txtnuevocentrodecosto.Text;
            string ced = textBox17.Text;
            //string cadena = "UPDATE VACAC SET ciudad='"+estado+ "' WHERE  ci="+ced ;
            conexion.Open();

            string query = "UPDATE VACAC SET FechaSalida = @fsalida WHERE CI = @ced";

            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@fsalida", fechasalida);

                // Asegúrate de asignar el valor de CI desde el TextBox como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ced", ced);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
                MessageBox.Show("Fecha de salida actualizada de: " + comboBox2.Text, "FEcha de salida Actualizada correctamente", MessageBoxButtons.OK);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");

            string posicionnue = txtnuevaposicion.Text;
            string ced = textBox17.Text;
            //string cadena = "UPDATE VACAC SET ciudad='"+estado+ "' WHERE  ci="+ced ;
            conexion.Open();

            string query = "UPDATE VACAC SET Posicion = @posc WHERE CI = @ced";

            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@posc", posicionnue);

                // Asegúrate de asignar el valor de CI desde el TextBox como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ced", ced);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
                MessageBox.Show("Posición actualizada de: " + comboBox2.Text, "Posición actualizada correctamente", MessageBoxButtons.OK);

            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.vACACTableAdapter1.FillBy(this.vacDataSet1.VACAC);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            decimal dias = Convert.ToDecimal(txtDiasPendientesSolicitar1.Text) - Convert.ToDecimal(txtDiasSolicitados1.Text);
            //MessageBox.Show(dias.ToString());
            if (dias>=0)
            {
                MessageBox.Show("Vacaciones Aprobadas" + comboBox2.Text, "Aprobadas", MessageBoxButtons.OK);
                //--------------------------------------------------------------------------------
                decimal diastomados1 = Convert.ToDecimal(txtprediastomados.Text) + Convert.ToDecimal(txtDiasSolicitados1.Text);
                txtposdiastomados.Text = diastomados1.ToString();
                decimal diaspendientes = Convert.ToDecimal(txtprediaspendientes.Text) - Convert.ToDecimal(txtDiasSolicitados1.Text);
                txtposdiaspendientes.Text = diaspendientes.ToString();
                decimal dpendpos =Convert.ToDecimal(txtposdiaspendientes.Text);

                //--------------------------------------------------------------------------------
                //--------------------------- INSERT HISTÓRICO -----------------------------------
                SqlConnection conexion1 = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");
                // INSERT TABLA HISTORICO
                string cedula = textBox6.Text;
                DateTime fechainicio = dtIniciovac.Value;
                DateTime fechafin = dtFinVac.Value; ;
                int diasTomados = Convert.ToInt32(txtDiasSolicitados1.Text);
                Decimal diasTomadospre = Convert.ToDecimal(txtprediastomados.Text);
                Decimal diasAcumuladospre = Convert.ToDecimal(txtprediasacumulados.Text);
                Decimal diasPendientespre = Convert.ToDecimal(txtprediaspendientes.Text);
                DateTime HfechaIngresoVacaciones = DateTime.Now;

               
                //string cadena = "UPDATE VACAC SET ciudad='"+estado+ "' WHERE  ci="+ced ;
                conexion1.Open();

                string query = "INSERT INTO historico (Hcedula , HfechaInicioVac,HfechaFinVac,HdiasTomados,HdiasAcumuladospre,HdiasTomadosPre,HdiasPendientesPre,HfechaIngresoVacaciones) " +
                               "VALUES (@Hcedula,@HfechaInicioVac,@HfechaFinVac,@HdiasTomados,@HdiasAcumuladospre,@HdiasTomadosPre,@HdiasPendientesPre,@HfechaIngresoVacaciones)";


                using (SqlCommand command = new SqlCommand(query, conexion1))
                {
                    // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                    command.Parameters.AddWithValue("@Hcedula", cedula);
                    command.Parameters.AddWithValue("@HfechaInicioVac", fechainicio);
                    command.Parameters.AddWithValue("@HfechaFinVac", fechafin);
                    command.Parameters.AddWithValue("@HdiasTomados", diasTomados);
                    command.Parameters.AddWithValue("@HdiasAcumuladospre", diasAcumuladospre);
                    command.Parameters.AddWithValue("@HdiasTomadosPre", diasTomadospre);
                    command.Parameters.AddWithValue("@HdiasPendientesPre", diasPendientespre);
                    command.Parameters.AddWithValue("@HfechaIngresoVacaciones", HfechaIngresoVacaciones);
                    // Ejecutar la consulta
                    command.ExecuteNonQuery();
                    MessageBox.Show("Vacaciones guardadas: " + comboBox2.Text, "Vacaciones guardadas", MessageBoxButtons.OK);    
                }
                //-------------------------------------------------------------------------------------
                //----------------------------- UPDATE VACACIONES -------------------------------------
                //-------------------------------------------------------------------------------------
                string query2 = "UPDATE VACAC SET DiasTomados = @DiasTomados, SaldoDiasPendiente = @SaldoDiasPendiente WHERE CI = @ced";
                using (SqlCommand command = new SqlCommand(query2, conexion1))
                {
                    // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                    command.Parameters.AddWithValue("@DiasTomados", diastomados1);
                    command.Parameters.AddWithValue("@SaldoDiasPendiente", dpendpos);
                    command.Parameters.AddWithValue("@ced", cedula);
                    // Ejecutar la consulta
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("Días insuficientes. Vacaciones no registradas" + comboBox2.Text, "Volver a ingresar fechas", MessageBoxButtons.OK);

                btnIngresarVacaciones.Enabled = false;
            }
           

            //---------------------------UPDATE--------------------------------

            /*
            string ciudad = txtnuevaciudad.Text;
            string ced = textBox17.Text;
            //string cadena = "UPDATE VACAC SET ciudad='"+estado+ "' WHERE  ci="+ced ;
            conexion.Open();

            string query = "UPDATE VACAC SET ciudad = @ciudadnue WHERE CI = @ced";

            using (SqlCommand command = new SqlCommand(query, conexion))
            {
                // Asegúrate de asignar el nuevo estado como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ciudadnue", ciudad);

                // Asegúrate de asignar el valor de CI desde el TextBox como un tipo de datos compatible con la columna
                command.Parameters.AddWithValue("@ced", ced);

                // Ejecutar la consulta
                command.ExecuteNonQuery();
                MessageBox.Show("Ciudad actualizada de: " + comboBox2.Text, "Ciudad Actualizada correctamente", MessageBoxButtons.OK);

            }
            */
            //-----------------------------------------------------------------------------------





        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void Información_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-OB05QDT;Initial Catalog=vac;Persist Security Info=True;User ID=sa;Password=sa");
            conexion.Open();
            textBox6.Text= comboBox3.SelectedValue.ToString();
            //textBox12.Text= comboBox1.SelectedValue.ToString();
            //string cod = comboBox1.;
            string cadena = "SELECT  ci,estado,ciudad,centroCosto,Posicion,FechaIngreso,FechaSalida,AntiuedadMeses,TotalDiasAcumulados,DiasTomados,SaldoDiasPendiente FROM VACAC  where ci=" + comboBox3.SelectedValue.ToString();
            //string cadena = "SELECT  ci,SaldoDiasPendiente FROM VACAC  where ci=" + comboBox1.SelectedValue.ToString();

            SqlCommand comando = new SqlCommand(cadena, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                txtDiasPendientesSolicitar1.Text = registro["SaldoDiasPendiente"].ToString();

                //--------------------------------------------------------------------------
                /*cedulatxt.Text = registro["ci"].ToString();
                estadotxt.Text = registro["estado"].ToString();
                ciudadtxt.Text = registro["ciudad"].ToString();
                centrocostotxt.Text = registro["centroCosto"].ToString();
                posiciontxt.Text = registro["Posicion"].ToString();
                fingresotxt.Text = registro["FechaIngreso"].ToString();
                fsalidatxt.Text = registro["FechaSalida"].ToString();
                antiguedadtxt.Text = registro["AntiuedadMeses"].ToString();
                diasacumuladostxt.Text = registro["TotalDiasAcumulados"].ToString();
                diastomadostxt.Text = registro["DiasTomados"].ToString();
                diaspendientestxt.Text = registro["SaldoDiasPendiente"].ToString();
                txtantiguedadac.Text = registro["AntiuedadMeses"].ToString();
                txtfingresoac.Text = registro["FechaIngreso"].ToString();
                txtdiasacumuladosac.Text = registro["TotalDiasAcumulados"].ToString();
                txtdiastomadosac.Text = registro["DiasTomados"].ToString();
                txtdiaspendientesac.Text = registro["SaldoDiasPendiente"].ToString();*/
                txtprediasacumulados.Text = registro["TotalDiasAcumulados"].ToString();
                txtprediastomados.Text = registro["DiasTomados"].ToString();
                txtprediaspendientes.Text = registro["SaldoDiasPendiente"].ToString();

                txtposdiasacumulados.Text = registro["TotalDiasAcumulados"].ToString();
                txtposdiastomados.Text = registro["DiasTomados"].ToString();
                txtposdiaspendientes.Text = registro["SaldoDiasPendiente"].ToString();


                //--------------------------------------------------------------------------



            }
            else
                MessageBox.Show("No existe un artículo con el código ingresado");
            conexion.Close();
        }

        private void dtFinVac_CursorChanged(object sender, EventArgs e)
        {
        }

        private void dtFinVac_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaini = dtIniciovac.Value;
            DateTime fechafin = dtFinVac.Value;
            double dias =1+ (fechafin - fechaini).TotalDays;
            txtDiasSolicitados1.Text = dias.ToString();
            btnIngresarVacaciones.Enabled = true;
        }
    }
}
