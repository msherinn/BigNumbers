namespace ConsoleApp1;

public class BigInteger
{
    private int[] _numbers;

    public BigInteger(string value)
    {
        _numbers = new int[value.Length];
        var j = 0;
        for (var i = value.Length - 1; i >= 0; i--)
        {
            _numbers[j] = value[i];
            j++;
        }
    }

    public int[] numbers => _numbers;

    public override string ToString()
    {
        var strNumber = "";
        for (var i = _numbers.Length - 1; i >=0; i--)
        {
            strNumber += Convert.ToChar(_numbers[i]);
        }

        return strNumber;
    }

    public int Length()
    {
        return _numbers.Length;
    }

    public BigInteger Add(BigInteger another)
    {
        var smaller = _numbers.Length < another.numbers.Length ? _numbers : another.numbers;
        var bigger = _numbers.Length > another.numbers.Length ? _numbers : another.numbers;
        var resultNumbers = new int[bigger.Length + 1];
        var remainder = new int();

        for (var i = 0; i < smaller.Length; i++)
        {
            var result = new int();
            if (remainder == 0)
            {
                result = bigger[i] + smaller[i];
            }

            else
            {
                result = bigger[i] + smaller[i] + 1;
            }

            if (result >= 10)
            {
                resultNumbers[i] = result - 10;
                remainder = 1;
            }

            else
            {
                resultNumbers[i] = result;
                remainder = 0;
            }
        }

        if (remainder != 0)
        {
            
        }

        return null;
    }

    /*public BigInteger Sub(BigInteger another)
    {
        
    }*/
}