using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
namespace ProjetoEvento.ClassePai.ClassesFilhas
{
    public class Cinema : Evento
    {
        public List<DateTime> Sessoes {get; set;}
        string Genero {get; set;}

        public Cinema(){

        }

        public Cinema(string Titulo, string Local, int Lotacao, string Duracao, int Classificacao, DateTime Data, List<DateTime> Sessoes, string Genero){
            base.Titulo = Titulo;
            base.Local = Local;
            base.Lotacao = Lotacao;
            base.Duracao = Duracao;
            base.Classificacao = Classificacao;
            base.Data = Data;
            this.Sessoes = Sessoes;
            this.Genero = Genero;
        }

        public override bool Cadastrar(){
            bool efetuado = false;
            StreamWriter arquivo = null;
            string sessao = "";
            foreach (var evento in Sessoes)
            {
                sessao += evento + "|";
            }
            sessao.Substring(0, Sessoes.Count-1);
            try{
                arquivo = new StreamWriter("cinema.csv", true);
                arquivo.WriteLine(
                    this.Titulo+";"+
                    this.Local+";"+
                    this.Duracao+";"+
                    this.Data+";"+
                    sessao+";"+
                    this.Genero+";"+
                    this.Lotacao+";"+
                    this.Classificacao
                );
                efetuado =true;
            } catch(Exception ex) {
                throw new Exception("Erro ao tentar gravar o arquivo. " + ex.Message);
            } finally {
                arquivo.Close();
            }
            return efetuado;
        }

        public override string Pesquisar(string Titulo){
            string resultado = "Título não encontrado";
            StreamReader ler = null;
            try{
                ler = new StreamReader("cinema.csv", Encoding.Default);
                string linha = "";
                while((linha=ler.ReadLine()) != null){
                    string[] dados = linha.Split(';');
                    if(dados[0] == Titulo){
                        resultado = linha;
                        break;
                    }
                }
            } catch (Exception ex) {
                resultado = "Erro ao tentar ler o arquivo. " + ex.Message;
            } finally {
                ler.Close();
            }
            return resultado;
        }

        public override List<string> Pesquisar(DateTime Data){
            List<string> resultado = new List<string>();
            //resultado.Add("Não tem nenhum evento para esta data");
            StreamReader ler = null;
            try{
                ler = new StreamReader("cinema.csv", Encoding.Default);
                string linha = "";
                while((linha = ler.ReadLine()) != null){
                    string[] dados = linha.Split(';');
                    if(DateTime.Parse(dados[3]) == Data){
                        resultado.Add(linha);
                    }
                }
                if (resultado.Count == 0){
                    resultado.Add("Não tem nenhum evento para esta data");
                }
            } catch (Exception ex) {
                resultado.Add("Erro ao tentar ler o arquivo. " + ex.Message);
            } finally {
                ler.Close();
            }
            return resultado;
        }

        public List<string> PesquisarSessao(string Sessao){
            //string resultado = "Elenco não encontrado";
            List<string> resultado = new List<string>();
            StreamReader ler = null;
            try{
                ler = new StreamReader("cinema.csv", Encoding.Default);
                string linha = "";
                while((linha=ler.ReadLine()) != null){
                    string[] dados = linha.Split(';');
                    string[] sessoes = dados[4].Split('-');
                    foreach(string evento in sessoes){
                        if(evento == Sessao){
                            resultado.Add(linha);
                            break;
                        }
                    }
                }
                if (resultado.Count == 0){
                    resultado.Add("Não encontramos nenhum evento com o integrante informado.");
                }
            } catch (Exception ex) {
                resultado.Add("Erro ao tentar ler o arquivo. " + ex.Message);
            } finally {
                ler.Close();
            }
            return resultado;
        }
    }
}