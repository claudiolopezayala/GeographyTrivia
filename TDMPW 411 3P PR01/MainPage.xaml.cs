namespace TDMPW_411_3P_PR01;

public partial class MainPage : ContentPage
{
	private List<Level> levels;
	private string CurrentQuestion = "";
    private string LevelStatus = "";
    private Level currentLevel;
    private bool isFirstTry = true;
    private int correctAnswers = 0;
    private int lives = 3;
    private string GameStatus = "First Game";

	public string currentQuestion
	{
		get => CurrentQuestion;
		set
		{
			CurrentQuestion = value;
			OnPropertyChanged();
		}
	}
    public string levelStatus
    {
        get => LevelStatus;
        set
        {
            LevelStatus = value;
            OnPropertyChanged();
        }
    }
    public string gameStatus
    {
        get => GameStatus;
        set
        {
            GameStatus = value;
            OnPropertyChanged();
        }
    }

	public MainPage()
    {
        BindingContext = this;
        loadGame();
        InitializeComponent();
    }

    private void loadGame()
    {
        lives = 3;
        correctAnswers = 0;
        loadLevels();
        loadNewLevel();
    }

    private void loadImageLives()
    {
        List<IView> ListLives = stackLives.ToList();
        foreach (IView ViewLive in ListLives)
        {
            (ViewLive as Image).Source = ImageSource.FromFile("heart.png");
        }
    }

    private void loadLevels()
    {
        levels = new List<Level>()
        {
            new Level() { question = "What is the name of the tallest mountain in the world?", answers = new List<string>(){"mount everest","everest"}},
            new Level() { question = "Which country has the largest population in the world?", answers = new List<string>(){"china"}},
            new Level() { question = "How many countries are there in the United Kingdom?", answers = new List<string>(){"4","four"}},
            new Level() { question = "What is the capital of Senegal?", answers = new List<string>(){"dakar"}},
            new Level() { question = "How many time zones does Russia have?", answers = new List<string>(){"11","eleven"}},
            new Level() { question = "What type of leaf is on the Canadian flag?", answers = new List<string>(){"maple","maple leaf"}},
            new Level() { question = "What season does Australia experience in December?", answers = new List<string>(){"summer"}},
            new Level() { question = "Peking Duck is the national dish of what country?", answers = new List<string>(){"china"}},
            new Level() { question = "In which European city was the first organized marathon held?", answers = new List<string>(){"athens"}},
            new Level() { question = "What country formerly ruled Iceland?", answers = new List<string>(){"denmark"}},
            new Level() { question = "Which continent is Cambodia located in?", answers = new List<string>(){"asia"}},
            new Level() { question = "What is the name of the longest river in Africa?", answers = new List<string>(){"nile","nile river","the nile river","the nile"}},
            new Level() { question = "What is the name of the largest country in the world?", answers = new List<string>(){"russia"}},
            new Level() { question = "What is the capital of Canada?", answers = new List<string>(){"ottawa"}},
            new Level() { question = "What is the name of the largest ocean in the world?", answers = new List<string>(){"pacific","pacific ocean","the pacific ocean","the pacific"}},
            new Level() { question = "What country are the Great Pyramids of Giza located in?", answers = new List<string>(){"egypt"}},
            new Level() { question = "What is the capital of Thailand?", answers = new List<string>(){"bangkok"}},
            new Level() { question = "What is the name of the smallest country in the world?", answers = new List<string>(){"vatican","the vatican","vatican city","the vatican city"}},
            new Level() { question = "What country has the most natural lakes?", answers = new List<string>(){"canada"}},
            new Level() { question = "How many States does the United States consist of?", answers = new List<string>(){"50","fifty"}},
            new Level() { question = "What is the capital of Jamaica?", answers = new List<string>(){"kingston"}},
            new Level() { question = "What is the only country that borders the United Kingdom?", answers = new List<string>(){"irleand"}},
            new Level() { question = "Mt. Fuji is the highest point located in which Asian country?", answers = new List<string>(){"japan"}},
            new Level() { question = "How many time zones does Australia have?", answers = new List<string>(){"3","three"}},
            new Level() { question = "What is the largest country in South America?", answers = new List<string>(){"brazil"}},
            new Level() { question = "What country ends with the letter Q?", answers = new List<string>(){"iraq"}},
            new Level() { question = "In which country is the Leaning Tower of Pisa located?", answers = new List<string>(){"italy"}},
            new Level() { question = "Which continent has no rivers?", answers = new List<string>(){"antartica"}},
            new Level() { question = "What is the capital of Madagascar?", answers = new List<string>(){"antananarivo","tana"}},
            new Level() { question = "What is the capital of Cuba?", answers = new List<string>(){"la habana","habana"}},
        };
    }

    private void loadNewLevel()
    {
        isFirstTry = true;
        selectLevel();
        displayLevel();
    }

    private void selectLevel()
    {
        Random rnd = new Random();
        int index = rnd.Next(levels.Count);
        currentLevel = levels[index];
        levels.RemoveAt(index);
    }

    private void displayLevel()
    {
        currentQuestion = currentLevel.question;
        levelStatus = "Unanswered";
    }

    private void Entry_Completed(object sender, EventArgs e)
    {
        if (validateAnswer())
        {
            correctAnswers++;
            if (correctAnswers >= 3)
            {
                handleWonGame();
            }
            else
            {
                loadNewLevel();
            }
        }
        else
        {
            if(isFirstTry)
            {
                levelStatus = "try again";
                isFirstTry = false;
            }
            else
            {
                handleLoseLive();
                loadNewLevel();
            }
        }
        entryAnswer.Text = "";
    }

    private bool validateAnswer()
    {
        return currentLevel.answers.Contains(entryAnswer.Text.ToLower());
    }

    private void handleLoseLive()
    {
        lives--;
        if (lives <= 0)
        {
            handleLoseGame();
        }
        else
        {
            List<IView> ListLives = stackLives.ToList();
            (ListLives[lives] as Image).Source = ImageSource.FromFile("transparent.png");
        }
    }

    private void handleLoseGame()
    {
        loadImageLives();
        gameStatus = "lost: Game Restarted";
        loadGame();
    }

    private void handleWonGame()
    {
        loadImageLives();
        gameStatus = "Won: Game Restarted";
        loadGame();
    }
}

