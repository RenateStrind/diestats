public class Die{
    public bool held;
    public int value;
    public bool locked;
}

public static class Game{
    public static List<Die> dice;
    static int held = 0;

    public static void NewGame(){
        dice = new List<Die>();
        for (int i = 0; i < 6; i++){
            dice.Add(new Die());
        }
        RollDice();
    }
    // public static void DisplayDebugStats(){
    //     Console.WriteLine($"held:{held}");
    //     Console.WriteLine(Game.IsRollable());
    // }
    public static void DisplayDice(){
        string dieSide = "----- ----- ----- ----- ----- -----";
        string dieCenter = $"| {dice[0].value} | | {dice[1].value} | | {dice[2].value} | | {dice[3].value} | | {dice[4].value} | | {dice[5].value} |";
        
        string options = "";

        foreach (Die die in dice)
        {
            if(die.locked){
                options += "      ";
            }
            else if (die.held){
                options+= " Held ";
            }else{
                options+= " Roll ";
            }
        }
        Console.Clear();
        Console.WriteLine(dieSide);
        Console.WriteLine(dieCenter);
        Console.WriteLine(dieSide);
        Console.WriteLine(options);
    }

    public static int GetScore(){
        int score=0;
        foreach (Die die in dice)
        {
            if(die.locked && die.value != 3){
                score+=die.value;
            }
        }
        return score;
    }

    public static void DisplayScore(){
        Console.WriteLine($"Score: {GetScore()}");
    }
    public static void DisplayMenu(){

        foreach (Die die in dice)
        {
            if(die.locked){
                Console.WriteLine("LOCKED");
            }
            else if (die.held){
                Console.WriteLine($"{dice.IndexOf(die)+1}) release");
            }else{
                Console.WriteLine($"{dice.IndexOf(die)+1}) hold");
            }
        }
        Console.WriteLine("\nr) Roll");
        Console.WriteLine("\nq) Quit");
    }
    public static bool IsRollable(){
        bool rollable = false;
        foreach (Die die in dice)
        {
            if(die.held && !die.locked){
                rollable = true;
                break;
            }
        }
        return rollable;
    }
    public static void RollDice(){
        Random r = new Random((int)DateTime.Now.Ticks);
        foreach (Die die in dice)
        {
            if(!die.held){
                die.value = r.Next(1,7);
            }else{
                die.locked = true;
            }
        }      
    }
    public static void ParseInput(string input){
        if(input == "r" && Game.IsRollable()){
            RollDice();
//            round+=1;
        }

        int b=0;
        int.TryParse(input,out b);
        if( b >= 1 && b <= 6 ){
            if(!dice[b-1].locked){
                dice[b-1].held = !dice[b-1].held;
                if(dice[b-1].held){held +=1;}
                else{held -=1;}
            }
        }
    }
    public static bool hasWon(){
        int locked=0;
        foreach (Die die in dice)
        {
            if(die.locked)
                locked += 1;
        }
        if (locked>=5){return true;}
        else{return false;}
    }
    public static string GetInput(string input=""){
        if(input==""){
            return Console.ReadLine();
        }else{
            return input;
        }
    }
}

