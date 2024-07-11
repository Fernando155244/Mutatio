using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mutation
{
    [Activity(Label = "AcPregunta")]
    public class AcPregunta : Activity
    {
        //Lista de preguntas fijas
        private String[] ds = 
        {
            "¿Cuántos han sido beneficiados?",
            "¿La página https://cambiosinterestatales.sep.gob.mx es para Cambios dentro del mismo Estado?", 
            "¿Hay diferencia entre el Proceso de Cambios y en el Proceso de Permutas de Adscripción de Estado a Estado?", 
            "¿El participar en los procesos garantiza que mi cambio o permuta sea autorizado?", 
            "¿En qué consiste el Proceso de Cambios de Adscripción de Estado a Estado?", 
            "¿En qué consiste el Proceso de Permutas de Adscripción de Estado a Estado?", 
            "¿Puedo elegir la ubicación en el Proceso de Cambios o Permutas de Adscripción de Estado a Estado?", 
            "¿Es posible que el sistema me registre en el proceso incorrecto, es decir, que inicie mi registro en cambios y me pase a permutas?", 
            "Me equivoque en el registro, seleccione incorrectamente Cambios cuando quiero solicitar Permuta o viceversa, ¿Qué puedo hacer para corregir?", 
            "No tengo el número de solicitud ya que no concluí el registro, ¿Cómo lo obtengo?", "¿No encuentro mi categoría en el combo del nivel educativo en el que me registré?", 
            "¿Cómo consulto o como se cuál es mi categoría?", 
            "Ya consulté mi categoría, revisando mi talón de pago y el Sistema me indica que no se encontraron resultados, ¿qué procede?", 
            "No puedo imprimir, ya que me aparece que no tengo nombramientos registrados, ¿qué hago?", 
            "Descargue mi solicitud, pero está en blanco ¿cómo lo soluciono?", "No ubico los datos para registrar mi unidad y subunidad, ¿Cómo o de donde los obtengo?", 
            "¿Participo en los Procesos de Cambios y Permutas de Adscripción de Estado a Estado solo con el registro?",
            "¿Que documentos debo anexar a mi solicitud?",
            "¿En dónde entrego mi solicitud y documentos?",
            "No me llega el correo de registro, ¿Qué pasa en este caso, como lo obtengo?",
            "Deseo cancelar mi participación en el Proceso de Cambios de Adscripción de Estado a Estado ¿Qué debo hacer?",
            "Deseo cancelar mi participación en el Proceso de Permutas de Adscripción de Estado a Estado ¿Qué debo hacer?"

        };
        private String[] dsLegend = 
        {
            "Error",
            "No, como su nombre lo indica es para Cambios y Permutas Interestatales o de Estado a Estado, los cambios al interno de los Estados son intraestatales y no se tiene participación ni injerencia en los mismos, ya que son facultad de las Secretarias de Educación de cada Estado.",
            "Si, si ya cuentan con una persona con la cual llevar a cabo el movimiento, deberán registrarse en Permutas, si no tienen con quien permutar deben registrarse en Cambios, esa es la diferencia.",
            "No, el hecho de participar en los Procesos no implica que el movimiento de cambio o permuta sea autorizado.",
            "La movilidad en este Proceso al igual que en el Proceso de permutas consiste en que, por un trabajador que salga de un Estado ingresara otro trabajador.\r\n\r\nEn este sentido, el Proceso de Cambios funciona con base al número de solicitudes que se reciben del personal interesado en algún cambio de Estado, y de ese modo, se llegue a generar el espacio que pueda ser ocupado por algún trabajador que solicite ingresar a ese Estado u otro y así sucesivamente, se van encadenando los movimientos; sin embargo, si no hay quien desee salir o ingresar a un Estado determinado, no existe la posibilidad de que pueda generarse algún movimiento.\r\n \r\nEstos movimientos (encadenamientos) se deben realizar en las mismas condiciones, como mismas categorías, niveles de carrera magisterial y demás cuestiones establecidas en la Convocatoria, dependiendo del nivel educativo que sé que trate, si no hay coincidencias entre Estados, categorías, niveles de carrera, estímulos y demás, no es posible se pueda autorizar algún movimiento.\r\n \r\nEs importante puntualizar que sabemos que el hecho de que participen en este Proceso es debido a que tienen alguna necesidad de cualquier índole y que por ello participan, sin embargo, este proceso depende del número de solicitudes que se reciban por Estado y nivel educativo, para que se pueda dar la movilidad.\r\n",
            "Estas son concertadas de común acuerdo entre los trabajadores, manifestando su consentimiento por medio de la solicitud de permuta; estas son procedentes siempre y cuando los involucrados cumplan en su totalidad con los requisitos, reglas y fechas establecidas en la Convocatoria correspondiente. En este proceso los trabajadores deben encontrar con quien llevar a cabo la permuta, ya que es de común acuerdo entre las partes sin intervención de un tercero.\r\n \r\nLa no procedencia de una permuta se debe al incumplimiento en alguno de los requisitos, reglas o fechas establecidas en la Convocatoria.\r\n",
            "No, si se llega autorizar su movilidad en alguno de los Procesos, la ubicación la determinará su nuevo Estado de adscripción conforme a las necesidades del servicio y estructura ocupacional, para que verifiquen bien las Convocatorias, ya que en ellas se especifica esta situación.\n\nNota: Antes de participar en los Procesos de Cambios o Permutas, deben leer detenidamente la Convocatoria correspondiente en su totalidad.",
            "No, esto se debe a que antes de iniciar el registro se presentan dos cuadros de diálogo en donde el trabajador tiene que colocar que esta de acuerdo en iniciar el registro en el Proceso que el mismo seleccionó ya sea Cambios o Permuta.",
            "Deberán solicitar la cancelación su registro actual por medio electrónico a la cuenta de correo: cambiosinter@nube.sep.gob.mx, con su nombre completo, RFC, número de solicitud a cancelar y motivo de cancelación (error al seleccionar proceso).",
            "Deberán realizar la misma acción, es decir, registrarse nuevamente y el Sistema le indicará que existe un RFC asociada a un número de solicitud y se lo mostrará.",
            "Se están registrando en el nivel incorrecto, deben seguir la indicación que se les presenta al inicio del registro e ingresar al apartado de consulta tu nivel educativo, en donde deberán colocar en el recuadro la categoría que ostentan y una vez que se indique el resultado de la búsqueda deberán registrarse nuevamente en el nivel educativo al cual corresponde su categoría, que en ocasiones no es el nivel educativo donde laboran.\r\nO bien si se encuentran en el nivel educativo correcto deben buscar en todo el combo.\r\n",
            "En el apartado de consulta tu nivel educativo, se muestra un ejemplo de la clave de cobro en donde se identifican los campos que componen dicha clave de cobro entre ellas la categoría (5 dígitos para docentes, 6 dígitos para personal de apoyo y asistencia a la educación).",
            "Deberá enviar un correo electrónico a cambiosinter@nube.sep.gob.mx informando la situación y recibirá respuesta de que procede en estos casos.",
            "Deberá verificar el manual de usuario, ya que no registro correctamente el apartado de centro de trabajo o de nombramientos, una vez que verifique el manual o los videos de apoyo, deberá ingresar al apartado modificar solicitud con su RFC y número de solicitud, que debe colocar correctamente, para concluir el registro y continuar con las acciones correspondientes para su participación.\r\n\r\n",
            "Se les recuerda lo establecido en el apartado de imprimir solicitud, donde se indica que para descargar correctamente su solicitud debe hacerlo por el explorador Firefox o bien Edge. \r\n\r\n\r\nPor lo anterior, deberá ingresar por cualquiera de estos dos exploradores para descargar correctamente su solicitud.\r\n",
            "Deberá ingresar al apartado de consulta tu nivel educativo, en donde viene un ejemplo de la clave de cobro, en donde se identifican los campos a registrar que, se verifican con del talón de pago en mano (Unidad, Subunidad, Categoría, Horas, Diagonal).",
            "No, deben verificar la Convocatoria en su totalidad, ya que en ella se indican los pasos a seguir para participar.",
            "Deben verificar la Convocatoria en su totalidad, ya que en ella se indica que documentos deben anexar en el apartado de requisitos para participar.",
            "Tal y como se indica en la Convocatoria, podrán consultar las instancias facultadas en el área de recursos humanos de la Secretaría de Educación en su Estado de adscripción actual.",
            "El correo de notificación del registro, no tienen ninguna injerencia en los trámites que debe hacer para su participación, es como se indica solo es una notificación, que no se puede recuperar y se reitera no tienen injerencia en la participación, por lo que el recibirlo o no, no afecta.",
            "1.\tSi solo tiene el registro y no ha hecho entrega de su solicitud o documentos.\r\n\r\nDebe enviar un correo a la cuenta: cambiosinter@nube.sep.gob.mx, con su nombre completo, RFC, número de solicitud a cancelar y motivo de cancelación; una vez que se confirme la recepción de esa información la cancelación se aplica en un lapso de tres a cinco días hábiles.\r\n\r\nSi los trabajadores quieren volver a realizar un registro, deberán estar al pendiente directamente en el Sistema en el tiempo antes mencionado para que verifiquen si ya pueden realizar un nuevo registro.\r\n\r\n2.\tSi ya hizo entrega de su solicitud y documentos en la instancia correspondiente en su Estado.\r\n\r\nTal y como se indica en la Convocatoria, debe solicitar la cancelación por escrito con los datos mínimos de: nombre completo, RFC, número de solicitud a cancelar y motivo de cancelación; el cual deberá entregarlo en la misma instancia en donde hizo entrega de su solicitud y documentos; posteriormente podrá enviar el acuse de dicho escrito a la cuenta: cambiosinter@nube.sep.gob.mx.\r\n\r\nUna vez que se confirme la recepción de su escrito, la cancelación se aplica en un lapso de tres a cinco días hábiles. \r\n\r\nSi los trabajadores quieren volver a realizar un registro, deberán estar al pendiente directamente en el Sistema en el tiempo antes mencionado para que verifiquen si ya pueden realizar un nuevo registro.\r\n",
            "1.\tSi solo tiene el registro y no ha asociado su solicitud ningún permutante.\r\n\r\nDebe enviar un correo a la cuenta: cambiosinter@nube.sep.gob.mx, con su nombre completo, RFC, número de solicitud a cancelar y motivo de cancelación; una vez que se confirme la recepción de esa información la cancelación se aplica en un lapso de tres a cinco días hábiles.\r\n\r\nSi los trabajadores quieren volver a realizar un registro, deberán estar al pendiente directamente en el Sistema en el tiempo antes mencionado para que verifiquen si ya pueden realizar un nuevo registro.\r\n\r\n2.\tSi ya solo tiene el registro, pero ya están asociadas las solicitudes con su o sus permutantes.\r\n\r\nDeberán todos los permutantes asociados al movimiento enviar un correo a la cuenta: cambiosinter@nube.sep.gob.mx, con su nombre completo, RFC, número de solicitud a cancelar y motivo de cancelación; una vez que se confirme la recepción de todas las peticiones de cancelación que integren la permuta, ésta se aplica en un lapso de tres a cinco días hábiles.\r\n\r\nSi los trabajadores quieren volver a realizar un registro, deberán estar al pendiente directamente en el Sistema en el tiempo antes mencionado para que verifiquen si ya pueden realizar un nuevo registro.\r\n\r\n3.\tSi los permutantes ya hicieron entrega de su solicitud y documentos en la instancia correspondiente en su Estado, o solo uno entregó y el otro u otros permutantes aún no lo hacen;  \r\n\r\nTal y como se indica en la Convocatoria, todos los permutantes deberán solicitar la cancelación por escrito con los datos mínimos de: nombre completo, RFC, número de solicitud a cancelar y motivo de cancelación.\r\n\r\nEl o los permutantes que hicieron entrega de solicitud y documentos deberán entregar sus escritos en la misma instancia en donde hicieron entrega de su solicitud y documentos; posteriormente podrá enviar el acuse de dicho escrito a la cuenta: cambiosinter@nube.sep.gob.mx.\r\n\r\nEl resto de los permutantes que no hubieran hecho de entrega de solicitud y documentos, solo deberán enviar su escrito a la cuenta de correo arriba mencionada y, una vez que se confirme la recepción de todos los escritos que integran la permuta, la cancelación se aplica en un lapso de tres a cinco días hábiles. \r\n\r\nSi los trabajadores quieren volver a realizar un registro, deberán estar al pendiente directamente en el Sistema en el tiempo antes mencionado para que verifiquen si ya pueden realizar un nuevo registro.\r\n"

        };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Variable para ingresar la posición anterior y posición de la lista
            int id = this.Intent.GetIntExtra("id", 0);
            // Create your application here

            SetContentView(Resource.Layout.Pregunta);
            //Index y definición de logo
            ImageView imgLogo = this.FindViewById<ImageView>(Resource.Id.Logo);
            imgLogo.SetImageResource(Resource.Drawable.Logo);

            //Indexados
            TextView Titulo = this.FindViewById<TextView>(Resource.Id.lblPreguntasTitulo);
            TextView Texto = this.FindViewById<TextView>(Resource.Id.txtPreguntasDatos);
            if (id > 0)
            {
                Titulo.Text = ds[id];
                Texto.Text = dsLegend[id];

            }
            else
            {
                Titulo.Text = "Error";
            }
        }
    }
}