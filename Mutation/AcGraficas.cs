using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OxyPlot.Xamarin.Android;
using System.Runtime.CompilerServices;
using System.Data;
using static Android.Icu.Text.Transliterator;

namespace Mutation
{
    [Activity(Label = "AcGraficas")]
    /*En esta pantalla podemos ver en graficas cuantos han sido veneficiados en este sistema*/
    public class AcGraficas : Activity
    {
        clsDatos datos = new clsDatos();
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            /*Variables para pruebas*/
            DataSet ds;
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Graficas);

            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            //Indexados
            PlotView Grafica = this.FindViewById<PlotView>(Resource.Id.GrafGraficas);
            ListView ListaPreguntas = this.FindViewById<ListView>(Resource.Id.lisGraficas);
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasTipoSolicitud);
            Spinner spEstatus = this.FindViewById<Spinner>(Resource.Id.spGraficasEstatus);
            Spinner spReferencias = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            Switch swcertificadas = this.FindViewById<Switch>(Resource.Id.swGraficasCertificadas);
            Switch swCanelada = this.FindViewById<Switch>(Resource.Id.swyGraficasCanceladas);
            Button Actualizar = this.FindViewById<Button>(Resource.Id.btnActualizar);
            Actualizar.Click += Actualizar_Click;
            //Variables de definición
            int certificada;
            int cancelar;

            //Función automatica del spinner
            LlenarTipo();
            LlenarEstatus();
            LlenarGraficas();

            //Definimos los valores segun lo que se decida en los switch
            if (swcertificadas.Selected)
            {
                certificada = 1;
            }
            else
            {
                certificada = 0;
            }
            if (swCanelada.Selected)
            {
                cancelar = 1;
            }
            else
            {
                cancelar = 0;
            }
            //Iniciamos el intento
            try
            {
                //Mandamos a llamar la función acincrona
                ds = await datos.graficadora(spReferencias.SelectedItemPosition, spTipo.SelectedItemPosition, certificada, cancelar, spEstatus.SelectedItemPosition);

                //Mandamos a llenar la lista y grafica
                ListaPreguntas.Adapter = new RellenarGraficas(this, ds, spReferencias.SelectedItemPosition);
                Grafica.Model = Estados(ds);
            }
            catch (Exception ex)//En  caso de fallar se manda un mensaje de alerta
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta!");
                a1.SetMessage($"Se ha detectado un error, comuníquese con departamento de sistemas\n{ex}");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();
            }
        }
        private void btnOK(object sender, DialogClickEventArgs e)
        {
            Finish();
        }

        private async void Actualizar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            //Indexados
            PlotView Grafica = this.FindViewById<PlotView>(Resource.Id.GrafGraficas);
            ListView ListaPreguntas = this.FindViewById<ListView>(Resource.Id.lisGraficas);
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasTipoSolicitud);
            Spinner spEstatus = this.FindViewById<Spinner>(Resource.Id.spGraficasEstatus);
            Spinner spReferencias = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            Switch swcertificadas = this.FindViewById<Switch>(Resource.Id.swGraficasCertificadas);
            Switch swCanelada = this.FindViewById<Switch>(Resource.Id.swyGraficasCanceladas);
            Button Actualizar = this.FindViewById<Button>(Resource.Id.btnActualizar);

            //Variables de definición
            int certificada;
            int cancelar;

            //Función automatica del spinner
            LlenarTipo();
            LlenarEstatus();
            LlenarGraficas();

            //Definimos los valores segun lo que se decida en los switch
            if (swcertificadas.Selected)
            {
                certificada = 1;
            }
            else
            {
                certificada = 0;
            }
            if (swCanelada.Selected)
            {
                cancelar = 1;
            }
            else
            {
                cancelar = 0;
            }
            //Iniciamos el intento
            try
            {
                //Mandamos a llamar la función acincrona
                ds = await datos.graficadora(spReferencias.SelectedItemPosition, spTipo.SelectedItemPosition, certificada, cancelar, spEstatus.SelectedItemPosition);

                //Mandamos a llenar la lista y grafica
                ListaPreguntas.Adapter = new RellenarGraficas(this, ds, spReferencias.SelectedItemPosition);
                Grafica.Model = Estados(ds);
            }
            catch (Exception ex)//En  caso de fallar se manda un mensaje de alerta
            {
                AlertDialog a1 = new AlertDialog.Builder(this).Create();
                a1.SetTitle("Alerta!");
                a1.SetMessage($"Se ha detectado un error, comuníquese con departamento de sistemas\n{ex}");
                a1.SetButton("Aceptar", btnOK);
                a1.Show();
            }
        }

        private void LlenarTipo()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasTipoSolicitud);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Todos");
            spInteres.Add("Cambios");
            spInteres.Add("Permutas");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private void LlenarEstatus()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasEstatus);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Todos");
            spInteres.Add("R");
            spInteres.Add("C");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private void LlenarGraficas()
        {
            Spinner spTipo = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            ArrayAdapter spInteres = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem);

            //Agregamos los valores de la tabla al arrayAdapter 
            spInteres.Add("Nivel Educativo");
            spInteres.Add("Estado");
            //Ingresamos el adapter al spinner
            spTipo.Adapter = spInteres;
        }
        private PlotModel Estados(DataSet ds)
        {
            Spinner spReferencias = this.FindViewById<Spinner>(Resource.Id.spGraficasReferencia);
            CategoryAxis ejex = new CategoryAxis();
            LinearAxis ejey = new LinearAxis();
            //al valor x le damos los nombres de los valores
            ejex.Position = AxisPosition.Bottom;
            //Le damos valores minimo y maximo
            ejey.Minimum = 0;
            ejey.Maximum = 0;
            ejey.Position = AxisPosition.Left;
            //Creamos los valores de las graficas
            ColumnSeries s1 = new ColumnSeries();


            if (spReferencias.SelectedItemPosition == 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ejex.Labels.Add(ds.Tables[0].Rows[i]["nivel"].ToString());
                    s1.Items.Add(new ColumnItem(Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString())));
                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString()) > ejey.Maximum)
                    {
                        ejey.Maximum = Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString());
                    }
                }
                //Creamos el modelo de graficas
                PlotModel m1 = new PlotModel();
                //Le damos titulo a la grafica
                m1.Title = "Productos";
                //Le damos los formatos de x e y
                m1.Axes.Add(ejey);
                m1.Axes.Add(ejex);
                //Le damos los valores a la grafica
                m1.Series.Add(s1);
                //Regresamos los valores ingresados 
                return m1;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ejex.Labels.Add(ds.Tables[0].Rows[i]["nombres"].ToString());
                    s1.Items.Add(new ColumnItem(Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString())));
                    if (Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString()) > ejey.Maximum)
                    {
                        ejey.Maximum = Convert.ToInt32(ds.Tables[0].Rows[i]["Cant"].ToString());
                    }
                }
                //Creamos el modelo de graficas
                PlotModel m1 = new PlotModel();
                //Le damos titulo a la grafica
                m1.Title = "Productos";
                //Le damos los formatos de x e y
                m1.Axes.Add(ejey);
                m1.Axes.Add(ejex);
                //Le damos los valores a la grafica
                m1.Series.Add(s1);
                //Regresamos los valores ingresados 
                return m1;
            }
        }
    }

    internal class RellenarGraficas : BaseAdapter
    {
        private AcGraficas acGraficas;
        private DataSet ds;
        private int Tipo;

        public RellenarGraficas(AcGraficas acGraficas, DataSet ds, int Tipo)
        {
            this.acGraficas = acGraficas;
            this.ds = ds;
            this.Tipo = Tipo;
        }

        public override int Count
        {
            get
            {
                return ds.Tables[0].Rows.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return "";
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View vista = convertView;
            //Declaración de vista
            if (vista == null)
            {
                //Aqui aparte de declarar los valores basicos declaramos el uso del diseño TabEmpresa para la fila
                vista = acGraficas.LayoutInflater.Inflate(Resource.Layout.ModGraficos, null);
            }
            TextView Estado = vista.FindViewById<TextView>(Resource.Id.txtTabEstado);
            TextView Numeros = vista.FindViewById<TextView>(Resource.Id.txtTabNumeros);
            if (Tipo == 0)
            {
                Estado.Text = ds.Tables[0].Rows[position]["Nivel"].ToString();
                Numeros.Text = ds.Tables[0].Rows[position]["Cant"].ToString();
            }
            else if (Tipo == 1)
            {
                Estado.Text = ds.Tables[0].Rows[position]["Nombre"].ToString();
                Numeros.Text = ds.Tables[0].Rows[position]["Cant"].ToString();
            }
            return vista;
        }
    }
}