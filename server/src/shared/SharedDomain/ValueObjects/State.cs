using System.ComponentModel.DataAnnotations;

namespace MyApp.SharedDomain.ValueObjects
{
    public sealed class State : ValueObject
    {
        private static readonly IDictionary<string, State> _states;

        static State()
        {
            const int STATE_COUNT = 29;
            _states = new Dictionary<string, State>(STATE_COUNT)
            {
                { "AC", new State("AC", "Acre") },
                { "AL", new State("AL", "Alagoas") },
                { "AP", new State("AP", "Amapá") },
                { "AM", new State("AM", "Amazonas") },
                { "BA", new State("BA", "Bahia") },
                { "CE", new State("CE", "Ceará") },
                { "DF", new State("DF", "Distrito Federal") },
                { "ES", new State("ES", "Espírito Santo") },
                { "GO", new State("GO", "Goiás") },
                { "MA", new State("MA", "Maranhão") },
                { "MT", new State("MT", "Mato Grosso") },
                { "MS", new State("MS", "Mato Grosso do Sul") },
                { "MG", new State("MG", "Minas Gerais") },
                { "PA", new State("PA", "Pará") },
                { "PB", new State("PB", "Paraíba") },
                { "PR", new State("PR", "Paraná") },
                { "PE", new State("PE", "Pernambuco") },
                { "PI", new State("PI", "Piauí") },
                { "RJ", new State("RJ", "Rio de Janeiro") },
                { "RN", new State("RN", "Rio Grande do Norte") },
                { "RS", new State("RS", "Rio Grande do Sul") },
                { "RO", new State("RO", "Rondônia") },
                { "RR", new State("RR", "Roraima") },
                { "SC", new State("SC", "Santa Catarina") },
                { "SP", new State("SP", "São Paulo") },
                { "SE", new State("SE", "Sergipe") },
                { "TO", new State("TO", "Tocantins") },
                { "XX", new State("XX", "Export") },
                { "EX", new State("EX", "Exterior") }
            };
        }

        private State(string abbreviation, string name)
        {
            Abbreviation = abbreviation;
            Name = name;
        }

        public static State Acre => GetByAbbreviation("AC");
        public static State Alagoas => GetByAbbreviation("AL");
        public static State Amapa => GetByAbbreviation("AP");
        public static State Amazonas => GetByAbbreviation("AM");
        public static State Bahia => GetByAbbreviation("BA");
        public static State Ceara => GetByAbbreviation("CE");
        public static State FederalDistrict => GetByAbbreviation("DF");
        public static State EspiritoSanto => GetByAbbreviation("ES");
        public static State Export => GetByAbbreviation("XX");
        public static State Exterior => GetByAbbreviation("EX");
        public static State Goias => GetByAbbreviation("GO");
        public static State Maranhao => GetByAbbreviation("MA");
        public static State MatoGrosso => GetByAbbreviation("MT");
        public static State MatoGrossoDoSul => GetByAbbreviation("MS");
        public static State MinasGerais => GetByAbbreviation("MG");
        public static State Para => GetByAbbreviation("PA");
        public static State Paraiba => GetByAbbreviation("PB");
        public static State Parana => GetByAbbreviation("PR");
        public static State Pernambuco => GetByAbbreviation("PE");
        public static State Piaui => GetByAbbreviation("PI");
        public static State RioDeJaneiro => GetByAbbreviation("RJ");
        public static State RioGrandeDoNorte => GetByAbbreviation("RN");
        public static State RioGrandeDoSul => GetByAbbreviation("RS");
        public static State Rondonia => GetByAbbreviation("RO");
        public static State Roraima => GetByAbbreviation("RR");
        public static State SantaCatarina => GetByAbbreviation("SC");
        public static State SaoPaulo => GetByAbbreviation("SP");
        public static State Sergipe => GetByAbbreviation("SE");
        public static State Tocantins => GetByAbbreviation("TO");
        public string Name { get; }
        public string Abbreviation { get; }

        public static State GetByAbbreviation(string abbreviation)
        {
            if (!_states.TryGetValue(abbreviation.ToUpperInvariant(), out var state))
            {
                throw new ValidationException($"Invalid state abbreviation: {abbreviation}");
            }

            return state;
        }

        public override object GetValue()
        {
            return Abbreviation;
        }

        public override string ToString()
        {
            return $"{Name} - {Abbreviation}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Abbreviation;
            yield return Name;
        }
    }
}

