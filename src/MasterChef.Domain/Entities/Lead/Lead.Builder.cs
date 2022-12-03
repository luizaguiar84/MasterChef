using System;

// ReSharper disable once CheckNamespace
namespace MasterChef.Domain.Entities
{
    public partial class Lead
    {
        public class Builder : ILeadBuilder
        {
            public Lead Instance { get; set; }

            public Builder()
            {
                this.Instance = new Lead();
            }
            public Builder(Lead instance)
            {
                this.Instance = instance;
            }

            ILeadBuilder ILeadBuilder.WithCreateDate(DateTime createDate)
            {
                Instance.DataCadastro = createDate;
                return this;
            }

            public ILeadBuilder WithLastUpdate(DateTime lastUpdate)
            {
                Instance.LastUpdate = lastUpdate;
                return this;
            }

            public ILeadBuilder WithNome(string nome)
            {
                Instance.Nome = nome;
                return this;
            }

            public ILeadBuilder WithPropaganda(bool aceitaPropaganda)
            {
                Instance.AceitaPropaganda = aceitaPropaganda;
                return this;
            }

            public ILeadBuilder WithEmail(string email)
            {
                Instance.Email = email;
                return this;
            }

            public ILeadBuilder WithTelefone(string telefone)
            {
                Instance.Telefone = telefone;
                return this;
            }

            public ILeadBuilder WithCupom(string cupom)
            {
                Instance.Cupom = cupom;
                return this;
            }
        }

        public interface ILeadBuilder
        {
            ILeadBuilder WithCreateDate(DateTime createDate);
            ILeadBuilder WithLastUpdate(DateTime lastUpdate);
            ILeadBuilder WithNome(string nome);
            ILeadBuilder WithPropaganda(bool aceitaPropaganda);
            ILeadBuilder WithEmail(string email);
            ILeadBuilder WithTelefone(string telefone);
            ILeadBuilder WithCupom(string cupom);

        }
    }
}