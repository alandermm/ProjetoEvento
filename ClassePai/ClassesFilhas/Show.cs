using System;
using System.IO;
namespace ProjetoEvento.ClassePai.ClassesFilhas
{
    public class Show : Evento
    {
        public string Artista {get; set;}
        public string GeneroMusical {get; set;}

        public Show(){

        }

        public Show(string Titulo, string Local, int Lotacao, string Duracao, int Classificacao, DateTime Data, string Artista, string GeneroMusical){
            base.Titulo = Titulo;
            base.Local = Local;
            base.Lotacao = Lotacao;
            base.Duracao = Duracao;
            base.Classificacao = Classificacao;
            base.Data = Data;
            this.Artista = Artista;
            this.GeneroMusical = GeneroMusical;
        }

        public override bool Cadastrar(){
            bool efetuado = false;
            StreamWriter arquivo = null;

            try{
                arquivo = new StreamWriter("show.csv", true);
                arquivo.WriteLine(
                    this.Titulo+";"+
                    this.Local+";"+
                    this.Duracao+";"+
                    this.Data+";"+
                    this.Artista+";"+
                    this.GeneroMusical+";"+
                    this.Lotacao+";"+
                    this.Classificacao
                );
                efetuado =true;
            } catch(Exception ex) {
                throw new Exception("Erro ao tentar gravar o arquivo " + ex.Message);
            } finally {
                arquivo.Close();
            }
            return efetuado;
        }
    }
}