

ManualPlay();
//Statistics();


//autoplay
//TODO: Define strategy as delegate so it can be passed in?
int AutoPlay(){
    Game.NewGame();
    while(true){
        if(Game.hasWon()){
            break;
        }
        Strategy1();
    }
    return Game.GetScore();
}

//strategy used for autoplay
void Strategy1(){

    //This strategy is only holding 3's
    //if no 3's hold a single lowest die

    int lowest = 10;
    int lowestIndex=0;
    foreach (Die die in Game.dice)
    {
        if(die.value == 3 && !die.locked){
            Game.ParseInput((Game.dice.IndexOf(die)+1).ToString());
        }else{
            if(lowest > die.value && !die.locked){
                lowest=die.value;
                lowestIndex = (Game.dice.IndexOf(die)+1);
            }
        }
    }
    if(!Game.IsRollable()){
        Game.ParseInput(lowestIndex.ToString());
    }
    Game.ParseInput("r");
}

//strategy used for autoplay
void Strategy2(){
    //This strategy is holding 3's and 1's
    //if no 3's or 1's hold a single lowest die

    int lowest = 10;
    int lowestIndex=0;
    foreach (Die die in Game.dice)
    {
        if((die.value == 3 || die.value == 1) && !die.locked){
            Game.ParseInput((Game.dice.IndexOf(die)+1).ToString());
        }else{
            if(lowest > die.value && !die.locked){
                lowest=die.value;
                lowestIndex = (Game.dice.IndexOf(die)+1);
            }
        }
    }
    if(!Game.IsRollable()){
        Game.ParseInput(lowestIndex.ToString());
    }
    Game.ParseInput("r");
}

void ManualPlay(){
    string input = "";

    Game.NewGame();

    while(input != "q"){
        Game.DisplayDice();
        if(Game.hasWon()){
            Game.DisplayScore();
            break;
        }
        Game.DisplayMenu();
        input = Game.GetInput();
        Game.ParseInput(input);
    }
}

void Statistics(){
    const int TOTALGAMES    = 1000000;
    int totalScore          = 0;
    int MaxScore            = int.MinValue;
    int MinScore            = int.MaxValue;
    int GameCount           = 0;
    var watch               = new System.Diagnostics.Stopwatch();

watch.Start();
while(GameCount<TOTALGAMES){
    int GameScore =AutoPlay();
    totalScore += GameScore;
    if(GameScore<MinScore){MinScore = GameScore;}
    if(GameScore>MaxScore){MaxScore = GameScore;}
    GameCount++;
}
watch.Stop();
Console.WriteLine($"ExecutionTime: {watch.ElapsedMilliseconds} ms\nTotalGames: {TOTALGAMES}\nAverageScore: {totalScore/TOTALGAMES}\nMaxScore: {MaxScore}\nMinScore: {MinScore}");
Console.ReadKey();

}