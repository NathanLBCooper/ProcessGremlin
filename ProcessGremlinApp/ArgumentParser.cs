namespace ProcessGremlinApp
{
    public class ArgumentParser
    {
        public bool TryParse(string[] args, out Arguments arguments)
        {
            string invokedVerb = null;
            object invokedVerbInstance = null;

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(
                args,
                options,
                (verb, subOptions) =>
                {
                    invokedVerb = verb;
                    invokedVerbInstance = subOptions;
                }) && invokedVerbInstance is CommonOptions)
            {
                arguments = new Arguments(invokedVerb, invokedVerbInstance);
                return true;
            }

            arguments = null;
            return false;
        }
    }
}