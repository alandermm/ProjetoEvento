using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ProjetoEvento.ClassePai.ClassesFilhas
{
    public class Teatro : Evento
    {
        public string[] Elenco {get; set;}
        public string Diretor {get; set;}

        public Teatro(){

        }

        public Teatro(string Titulo, string Local, int Lotacao, string Duracao, int Classificacao, DateTime Data, string[] Elenco, string Diretor){
            base.Titulo = Titulo;
            base.Local = Local;
            base.Lotacao = Lotacao;
            base.Duracao = Duracao;
            base.Classificacao = Classificacao;
            base.Data = Data;
            this.Elenco = Elenco;
            this.Diretor = Diretor;
        }

        public override bool Cadastrar(){
            bool efetuado = false;
            StreamWriter arquivo = null;
            string elenco = "";
            foreach (var integrante in Elenco)
            {
                elenco += integrante + "-";
            }
            elenco.Substring(0, Elenco.Length-1);
            try{
                arquivo = new StreamWriter("teatro.csv", true);
                arquivo.WriteLine(
                    this.Titulo+";"+
                    this.Local+";"+
                    this.Duracao+";"+
                    this.Data+";"+
                    elenco+";"+
                    this.Elenco+";"+
                    this.Diretor+";"+
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
                ler = new StreamReader("teatro.csv", Encoding.Default);
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
                ler = new StreamReader("teatro.csv", Encoding.Default);
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

        public List<string> PesquisarIntegrante(string Integrante){
            //string resultado = "Elenco não encontrado";
            List<string> resultado = new List<string>();
            StreamReader ler = null;
            try{
                ler = new StreamReader("teatro.csv", Encoding.Default);
                string linha = "";
                while((linha=ler.ReadLine()) != null){
                    string[] dados = linha.Split(';');
                    string[] elenco = dados[4].Split('-');
                    foreach(string integ in elenco){
                        if(integ == Integrante){
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