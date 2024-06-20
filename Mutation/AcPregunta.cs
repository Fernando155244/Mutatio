﻿using Android.App;
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
        private String[] ds = { "¿Cuántos han sido beneficiados?"
                , "¿Cuanto tarda la transición?",
            "¿Tengo que tener casa alla antes de hacer la certificación?",
            "¿Me regalan el departamento?",
            "¿Ustedes pagan la mudanza?" };
        private String[] dsLegend = {"Error",
            "La transición tardara lo que tenga que tardar",
            "Si y Regalarle una al programador de esta app",
            "Lorem ipsum dolor sit, amet consectetur adipisicing elit. Ducimus natus unde ullam modi fugit quo voluptates eaque eius delectus error doloremque quia animi, quidem aspernatur enim! Amet debitis officia aut.\r\nInventore exercitationem iste aperiam voluptates aliquam, quam eum facilis fugiat eaque nisi labore cupiditate, officiis veritatis alias repellendus totam error accusantium reiciendis doloremque? Doloremque id aliquid eum esse expedita blanditiis!\r\nMaiores accusantium tempora cupiditate ea, fugit ipsam ab consequatur culpa excepturi temporibus magnam non rem debitis tenetur, iure cumque a, unde mollitia earum ipsum ut obcaecati veniam sunt veritatis! Magnam!\r\nMollitia, saepe repellat quas totam, dolor, quibusdam minus enim voluptas laborum atque voluptate non dolore quia distinctio. Reiciendis officiis aut odio, minus sunt aliquam dolore cumque! Commodi facere veniam placeat!\r\nQuo et tempora eveniet, ipsam eaque error deleniti fugiat quasi pariatur aperiam quod temporibus eum doloribus voluptatibus iste cum sit mollitia saepe reiciendis quaerat enim? Officia error rem quasi distinctio!\r\nLabore eveniet, dignissimos consequatur quas soluta nulla sunt commodi reprehenderit id magnam ab natus autem non dolore quaerat fugit eligendi voluptatum officia accusamus. Dignissimos veritatis deserunt voluptatibus at ea porro?\r\nNulla neque pariatur laudantium qui eos, recusandae repudiandae magni architecto minima corrupti amet numquam doloribus, excepturi doloremque id vero ipsum deleniti, quos vitae optio obcaecati? Pariatur possimus officia maxime deleniti.\r\nImpedit rem molestiae quibusdam unde? Tempora iste aperiam voluptate doloribus eveniet fugiat est qui, repudiandae quia tenetur iusto culpa tempore, praesentium magni enim! Consequatur ea, blanditiis libero aperiam quisquam inventore!\r\nAccusamus suscipit distinctio rem totam qui inventore voluptatibus? Repellendus quidem perspiciatis et sapiente, qui at error, nam aspernatur nobis tenetur numquam hic quos suscipit distinctio reprehenderit placeat vel nesciunt? Quam.\r\nNostrum inventore laborum esse veniam dolorum, vitae similique porro, assumenda maxime, odit harum. Ad animi quia, quidem vel totam excepturi maxime dolorum quis nesciunt sequi aut error veniam dolores architecto!\r\nRecusandae quaerat eum ea, laudantium sit natus odit similique aperiam facilis ipsum nostrum sint dolorem pariatur ad possimus eius? Debitis accusantium in, sint pariatur eum inventore qui distinctio quaerat earum!\r\nSit accusamus magni perspiciatis in. Blanditiis dicta, temporibus repudiandae odio harum omnis velit atque cupiditate aliquid delectus, ducimus maiores nobis esse maxime aliquam eius odit ipsam. Aperiam asperiores ullam dolorum.\r\nVoluptates, incidunt ad dolorum omnis porro commodi rerum maxime aliquam facilis! Sint laborum modi, unde cumque voluptate eum ratione nemo alias similique aliquam incidunt nulla voluptatum velit fuga repudiandae iusto!\r\nQuibusdam eveniet enim, eligendi saepe, laboriosam quod, vel hic excepturi odit pariatur animi minima quas error provident! Omnis saepe ipsa ratione, cum, modi, veritatis tempora aperiam eius odit voluptates iusto?\r\nDebitis veritatis sunt possimus delectus, laboriosam culpa aliquam? Voluptate voluptatibus dolore ut tempora incidunt provident aspernatur officia, ipsam commodi, ad fuga corporis magni natus porro! Laboriosam cupiditate possimus numquam quisquam!\r\nAb magni exercitationem quibusdam facere praesentium, est quas, sit commodi atque debitis in modi quis nesciunt sunt mollitia veritatis voluptatum qui quaerat reiciendis. Voluptatum, nobis fuga. Voluptate odio nihil in.\r\nVel delectus maxime ratione, cupiditate, beatae nisi non, rem animi earum aspernatur soluta dicta facilis obcaecati veniam quibusdam eligendi! Enim vitae quibusdam nemo labore ratione corporis deserunt nam, rerum maiores.\r\nVoluptatum aut reprehenderit obcaecati, possimus odit nobis nisi! Magnam cum facere eum vero soluta sapiente tempore, aliquid odio earum, fugit quis voluptates fugiat non. Assumenda error dicta aut quia sapiente.\r\nSuscipit, quos eos ex incidunt illo iste! Voluptatem quas dicta a. Magnam optio unde a natus ut possimus, dignissimos temporibus, placeat, excepturi est dolorum culpa ipsam quae soluta facilis beatae!\r\nCorporis aspernatur neque dolor minus rerum natus vitae perferendis tempora maiores atque aperiam labore deserunt repudiandae quos hic voluptate velit iure cum, porro rem. Labore doloremque optio cum soluta id!\r\nExcepturi labore libero fuga a necessitatibus quo dolorum! Incidunt necessitatibus doloribus aliquam magni amet mollitia velit, non, illo laborum enim repellendus itaque cupiditate provident quas, nemo ratione voluptas rem voluptates.\r\nTotam, sapiente sit error ab veritatis iste! Nemo rem quas tempora ullam veritatis, similique placeat numquam, veniam impedit repellendus quasi minima adipisci assumenda illo laboriosam reprehenderit quis est, magnam itaque!\r\nHic iure odit tempore nostrum ex impedit, vitae eius unde officia enim aspernatur repellat fuga ipsa consectetur tempora! Impedit amet nihil voluptatum nisi ad laborum repellat qui eum alias rem!\r\nPorro enim doloribus rerum sit dolorem nam ullam adipisci magnam laborum voluptatibus illum nostrum, provident voluptatum vitae laboriosam? Ducimus vero ratione maxime veniam consectetur. Quas enim ipsam commodi iure id?\r\nTempora, doloremque quae facilis eos, assumenda ipsa tempore harum nostrum quia ab autem iure laudantium omnis et consectetur? Ipsa mollitia dolore suscipit quod doloremque ea culpa nam, possimus dicta facilis.\r\nNisi molestiae libero dolorem voluptatibus quos illo nam eveniet, at aut consectetur porro doloribus itaque voluptate architecto, quae commodi dolores! Reiciendis eum voluptate repellat, consequatur veritatis nobis iste blanditiis cum?\r\nVoluptatem nihil dignissimos tenetur earum dicta perspiciatis ea qui quia non incidunt excepturi quis voluptatibus ex recusandae, illo magnam cupiditate ipsum molestias dolorum provident reprehenderit quisquam obcaecati. Blanditiis, vero optio.\r\nNon atque sed iure modi porro et quae quis autem consequatur. Non quaerat fugiat velit porro nostrum eaque voluptatum quas quam repudiandae, explicabo ducimus officia corporis, quidem qui minima dolores.\r\nNesciunt laborum beatae sequi fugiat atque placeat ipsam iure veniam numquam, exercitationem, aspernatur dolore voluptates rem consequatur sunt possimus rerum cupiditate accusantium nihil vitae, modi distinctio aut odit omnis? Quaerat?\r\nQuis beatae ipsum nam incidunt excepturi exercitationem facere dignissimos, accusamus minima veritatis velit? Sunt, optio debitis consequatur dignissimos atque aliquid cupiditate! Iusto magnam culpa optio, consectetur dicta odit? Reprehenderit, sit!\r\nIpsam dolor omnis perspiciatis labore quibusdam reprehenderit officia consequatur saepe quia! Excepturi voluptatum debitis ullam animi sit perferendis blanditiis itaque deserunt? Numquam architecto temporibus mollitia aliquam atque eius reprehenderit incidunt?\r\nSuscipit adipisci sapiente corrupti non, magnam explicabo eligendi animi aspernatur id distinctio tenetur ea accusantium quasi sequi voluptas ducimus quas eos ratione nobis, quae dolorum sit voluptatibus. Aut, cupiditate tempora?\r\nEum numquam esse dolorem non expedita earum libero aspernatur et quae amet, mollitia molestiae alias incidunt beatae quo. Delectus mollitia corporis eius iure adipisci voluptas officia modi dolores similique consequuntur!\r\nNulla commodi esse optio. Delectus reprehenderit repellendus doloremque iure sit iusto totam enim est ratione quasi laboriosam, adipisci illum asperiores sed ab, at perferendis consectetur aliquam cum in deserunt fugit.\r\nAtque tempore distinctio quas. Magni quis cupiditate ducimus amet molestiae quo quia laborum rerum rem asperiores animi maiores natus provident labore corporis nulla laudantium, porro ipsum eos sunt. Quae, amet?\r\nError dolor atque quaerat voluptate, exercitationem nostrum ut! Nobis laudantium iure, ut, vel ipsam reiciendis nostrum accusantium enim recusandae ex nulla a atque maiores officiis voluptatum porro excepturi, assumenda autem.\r\nEveniet, maiores necessitatibus vitae quis, dignissimos qui saepe praesentium possimus, tempora esse quod nesciunt dolores eligendi mollitia unde voluptas animi totam maxime iure quo libero atque harum! Facere, dignissimos aliquam.\r\nExcepturi illum aspernatur, itaque, debitis ducimus est laboriosam necessitatibus repellat harum magni dolores impedit, sint id! Maxime reprehenderit alias voluptas, velit obcaecati inventore commodi odio ipsa tenetur, est impedit ducimus?\r\nRecusandae ratione ea dolores corporis eos non excepturi impedit, explicabo deserunt rem magni tempora. Architecto non harum cumque optio soluta facere dolorem? Minima, nostrum! Sequi exercitationem molestias ad ducimus unde?\r\nExercitationem amet harum accusamus eos, ratione, quaerat debitis, assumenda repudiandae suscipit quam architecto quis. Exercitationem consectetur sapiente officia tempora molestiae repellendus officiis fuga. Temporibus hic enim magni dolor illo quia.\r\nLaborum voluptatum temporibus praesentium impedit ad earum magnam quasi dolores aliquam. Omnis quia ad fugit recusandae. Repellat neque, quibusdam, ipsam numquam dolorem illo repellendus consequuntur quos veritatis excepturi, doloremque amet.\r\nPariatur perspiciatis fugit nemo porro nisi a maiores nostrum impedit sed cupiditate eum nam deserunt minus aperiam qui quod accusamus ad repellendus, non praesentium asperiores? Unde, obcaecati corrupti. Saepe, fuga?\r\nOfficiis quidem repellat dolore pariatur quisquam. Nemo iure error rerum dolorem ipsum tempora? Quae modi quia ex eius recusandae doloremque debitis quod, blanditiis a doloribus natus accusantium, distinctio in est!\r\nPerferendis repellat eveniet fugiat nemo voluptas, soluta ex illum, vel tempora autem voluptatum nisi laboriosam ducimus quibusdam beatae quasi delectus nulla tempore sed rerum. Quae, consequatur natus! Velit, veritatis natus!\r\nDebitis ullam accusamus provident aliquid, suscipit blanditiis facilis odio odit. Vitae commodi earum voluptas explicabo aperiam voluptatibus minus, culpa nemo sint. Minima officiis qui voluptate fuga, harum facilis beatae delectus!\r\nCommodi deserunt ea, modi fugiat accusantium explicabo adipisci architecto distinctio culpa possimus delectus quo perspiciatis in voluptate recusandae doloremque reiciendis nulla enim fuga quisquam eius veniam molestias? Incidunt, tempora minus?\r\nQuam, nesciunt aliquid? Porro repellat, accusamus quod exercitationem hic ullam pariatur, esse soluta ex, ratione voluptate eius error! Vel recusandae laborum molestias qui totam, ipsa odio iure harum nihil exercitationem!\r\nA quo architecto sequi quod rem voluptates, eligendi accusamus sapiente impedit atque. Similique labore quia facilis consequatur eveniet minus beatae obcaecati porro? Sed, architecto voluptates? Quas pariatur recusandae debitis aperiam?\r\nUnde eum repellat, tempore eveniet at magni minus pariatur in dignissimos culpa ex fugit impedit quos distinctio, quas eaque reiciendis, totam fugiat quibusdam itaque id? Eius expedita omnis odio quaerat.\r\nEst maxime porro facilis quas, praesentium illo iure quos recusandae, vel esse aut, nihil enim? Cum vero consequatur fugiat obcaecati dicta nobis, at architecto voluptatibus praesentium commodi necessitatibus earum autem!",
            "Si, pero con derecho a romper sus vasos"};
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