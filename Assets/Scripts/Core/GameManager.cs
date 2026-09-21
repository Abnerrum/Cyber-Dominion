using UnityEngine;

namespace CyberDominion
{
    public enum GameState { Menu, City, Battle, Paused, GameOver }

    [DefaultExecutionOrder(-100)]
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public GameState State { get; private set; } = GameState.Menu;

        public ResourceManager Resources { get; private set; }
        public BuildingManager Buildings { get; private set; }
        public CombatManager Combat { get; private set; }
        public SaveManager Saves { get; private set; }
        public UIManager UI { get; private set; }
        public AudioManager Audio { get; private set; }

        private GameState stateBeforePause;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            Resources = GetComponent<ResourceManager>();
            Buildings = GetComponent<BuildingManager>();
            Combat = GetComponent<CombatManager>();
            Saves = GetComponent<SaveManager>();
            UI = GetComponent<UIManager>();
            Audio = GetComponent<AudioManager>();

            Resources.Initialize(this);
            Buildings.Initialize(this);
            Combat.Initialize(this);
            UI.Initialize(this);
            ChangeState(GameState.Menu);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && State != GameState.Menu)
                TogglePause();
        }

        public void NewGame()
        {
            Time.timeScale = 1f;
            Resources.ResetValues();
            Buildings.ResetCity();
            Buildings.PlaceHeadquarters();
            Combat.ResetProgress();
            ChangeState(GameState.City);
            Saves.SaveGame();
            UI.ShowMessage("Comandante, construa sua base e prepare suas tropas!");
        }

        public void ContinueGame()
        {
            GameSaveData data = Saves.LoadGame();
            if (data == null)
            {
                UI.ShowMessage("Nenhum salvamento encontrado.");
                return;
            }

            Time.timeScale = 1f;
            Resources.Load(data);
            Buildings.Load(data.buildings);
            Combat.SetMission(data.currentMission);
            ChangeState(GameState.City);
            UI.ShowMessage("Partida carregada.");
        }

        public void StartMission()
        {
            if (State != GameState.City) return;
            ChangeState(GameState.Battle);
            Combat.BeginMission();
        }

        public void FinishMission(bool victory)
        {
            ChangeState(GameState.City);
            if (victory)
            {
                Resources.Add(150 + Combat.CurrentMission * 50, 20, 30);
                UI.ShowMessage("Vitória! Recompensas adicionadas à base.");
                Audio.PlayTone(760f, 0.18f);
                Saves.SaveGame();
            }
            else
            {
                UI.ShowMessage("Retirada concluída. Fortaleça sua base e tente novamente.");
            }
        }

        public void TogglePause()
        {
            if (State == GameState.Paused)
            {
                Time.timeScale = 1f;
                ChangeState(stateBeforePause);
            }
            else
            {
                stateBeforePause = State;
                Time.timeScale = 0f;
                ChangeState(GameState.Paused);
            }
        }

        public void ReturnToMenu()
        {
            Time.timeScale = 1f;
            Saves.SaveGame();
            ChangeState(GameState.Menu);
        }

        public void ChangeState(GameState newState)
        {
            State = newState;
            Buildings.SetCityVisible(newState == GameState.City || (newState == GameState.Paused && stateBeforePause == GameState.City));
            UI.RefreshState(newState);
        }

        public static Sprite CreateSolidSprite(Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.name = "RuntimeColor";
            texture.SetPixel(0, 0, color);
            texture.Apply();
            return Sprite.Create(texture, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1f);
        }

        private void OnApplicationQuit()
        {
            if (Saves != null && State != GameState.Menu) Saves.SaveGame();
        }
    }
}
