namespace DrugLord.Messages
{
    public class SecretCodeHasBeenGenerated
    {
        public string Code { get; set; }

        public SecretCodeHasBeenGenerated(string code)
        {
            Code = code;
        }
    }
}