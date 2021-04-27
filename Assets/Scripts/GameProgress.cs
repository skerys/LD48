using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct ProgressEvent
{
    public Func<bool> method;
    public float progressNeeded;
    public bool priority;
}

public class GameProgress : MonoBehaviour
{
    bool endingWitnessed = false;
    List<ProgressEvent> progressEvents;
    int currentEventIndex;
    bool firstRunOfEvent = true;
    public float progressPercentagePerSecond = 1f;
    public SpriteRenderer exteriorBackground;
    public float amountToScrollBG = 78f;
    public GameObject progressIndicator;
    public float amountToScrollIndicator = 2.75f;
    float progressValue = 0f;

    float backgroundHeight;
    float bgInitialY;
    float progressIndicatorInitialY;

    bool progressPaused = false;

    public SpriteRenderer deathOverlay;

    public AsteroidSpawner asteroidSpawner;
    public DroneMinigame droneMinigame;
    public ShipAngle shipAngle;
    public ShipHealth shipHealth;
    public ConsoleScreen consoleScreen;
    public TiltSlider tiltSlider;
    public Button buttonTop;
    bool topPressed = false;
    public Button buttonBottom;
    bool bottomPressed = false;
    public Switch consoleSwitch;
    bool consoleSwitched = false;

    public GameObject asteroidGuide;
    public GameObject switchGuide;
    public GameObject balanceGuide;
    public GameObject droneGuide;

    bool gameOver = false;
    bool canGoToNextEvent = false;
    void Start()
    {
        backgroundHeight = exteriorBackground.sprite.bounds.extents.y * 2f;
        bgInitialY = exteriorBackground.transform.position.y;
        progressIndicatorInitialY = progressIndicator.transform.position.y;

        progressEvents = new List<ProgressEvent>();

        progressEvents.Add(new ProgressEvent{method = EventStart, progressNeeded = 0});

        progressEvents.Add(new ProgressEvent{method = EventBalanceIntro, progressNeeded = 0.0f, priority = false});

        progressEvents.Add(new ProgressEvent{method = EventQuiz1, progressNeeded = 0, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventQuiz2, progressNeeded = 0, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventQuiz3, progressNeeded = 0, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventQuiz4, progressNeeded = 0, priority = false});

        progressEvents.Add(new ProgressEvent{method = EventAsteroidIntro, progressNeeded = 0.15f, priority = true});

        progressEvents.Add(new ProgressEvent{method = EventStrongWinds, progressNeeded = 0.22f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventStrongWindsEnd, progressNeeded = 0.3f, priority = false});

        progressEvents.Add(new ProgressEvent{method = EventAsteroidsExtra, progressNeeded = 0.35f, priority = false});

        progressEvents.Add(new ProgressEvent{method = EventDroneIntro1, progressNeeded = 0.4f, priority = true});
        progressEvents.Add(new ProgressEvent{method = EventDroneIntro2, progressNeeded = 0.4f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventDroneIntro3, progressNeeded = 0.4f, priority = false});

        progressEvents.Add(new ProgressEvent{method = EventJokes1, progressNeeded = 0.5f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventJokes2, progressNeeded = 0.5f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventJokes3, progressNeeded = 0.5f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventJokes4, progressNeeded = 0.5f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventJokes5, progressNeeded = 0.5f, priority = false});

        progressEvents.Add(new ProgressEvent{method = EventDroneIntro1, progressNeeded = 0.66f, priority = true});
        progressEvents.Add(new ProgressEvent{method = EventDroneIntro2, progressNeeded = 0.66f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventDroneIntro3NoTutorial, progressNeeded = 0.66f, priority = false});

        progressEvents.Add(new ProgressEvent{method = EventAsteroidsExtra2, progressNeeded = 0.82f, priority = true});

        progressEvents.Add(new ProgressEvent{method = EventEnding1, progressNeeded = 0.99f, priority = true});
        progressEvents.Add(new ProgressEvent{method = EventEnding2, progressNeeded = 0.99f, priority = false});
        progressEvents.Add(new ProgressEvent{method = EventEnding3, progressNeeded = 0.99f, priority = false});

        currentEventIndex = 0;
    }

    void OnEnable()
    {
        buttonTop.OnButtonPress += TopPressed;
        buttonBottom.OnButtonPress += BottomPressed;
        consoleSwitch.OnSwitchChange += ConsoleSwitched;
    }

    void OnDisable()
    {
        buttonTop.OnButtonPress -= TopPressed;
        buttonBottom.OnButtonPress -= BottomPressed;
        consoleSwitch.OnSwitchChange -= ConsoleSwitched;
    }

    void Update()
    {
        bool blocked = false;
        if(!gameOver)
        {
            blocked = progressEvents[currentEventIndex].method();
        }
        else
        {
            blocked = EventDeath();
        }
        canGoToNextEvent |= blocked;
        
        if(!progressPaused) progressValue += progressPercentagePerSecond / 100f * Time.deltaTime;
        if(progressValue > 1.01f) progressValue = 1.01f;
        
        if(currentEventIndex < progressEvents.Count -1 && (canGoToNextEvent || progressEvents[currentEventIndex+1].priority) && progressValue >= progressEvents[currentEventIndex+1].progressNeeded)
        {
            firstRunOfEvent = true;
            currentEventIndex++;
            canGoToNextEvent = false;
        }

        if(!gameOver && (shipHealth.GetHealth() <= 0 || droneMinigame.oxygenAmount < 0.001f))
        {
            gameOver = true;
            firstRunOfEvent = true;
        }

        float newY = Mathf.Lerp(bgInitialY, bgInitialY + amountToScrollBG, progressValue);
        float newYProgressIndicator = Mathf.Lerp(progressIndicatorInitialY, progressIndicatorInitialY - amountToScrollIndicator, progressValue);

        exteriorBackground.transform.position = new Vector2(exteriorBackground.transform.position.x, newY);
        progressIndicator.transform.position = new Vector2(progressIndicator.transform.position.x, newYProgressIndicator);
    }

    void TopPressed()
    {
        topPressed = true;
    }

    void BottomPressed()
    {
        bottomPressed = true;
    }

    void ConsoleSwitched()
    {
        consoleSwitched = true;
    }

    void LateUpdate()
    {
        topPressed = false;
        bottomPressed = false;
        consoleSwitched = false;
    }

    bool EvaluateTopButton()
    {
        if(topPressed)
        {
            topPressed = false;
            return true;
        }
        else return false;
    }

    bool EvaluateBottomButton()
    {
        if(bottomPressed)
        {
            bottomPressed = false;
            return true;
        }
        else return false;
    }

    bool EvaluateConsoleSwitched()
    {
        if(consoleSwitched)
        {
            consoleSwitched = false;
            return true;
        }
        else return false;
    }

    bool EventStart()
    {
        if(firstRunOfEvent)
        {
            progressPaused = true;
            shipAngle.stopTilt = true;
            consoleScreen.StartMessage("Deep Jupiter\n\na game made for LD 48\n\n\n< Press to\n  begin\n  mission");
            firstRunOfEvent = false;
        }

        if(!consoleScreen.isWriting && EvaluateBottomButton())
        {
            progressPaused = false;
            consoleScreen.Clear();
            return true;
        }
        return false;
    }

    Coroutine routineReference;    

    bool EventBalanceIntro()
    {
        if(firstRunOfEvent)
        {
            shipAngle.stopTilt = false;
            consoleScreen.StartMessage("Keep the\nlander\nlevel!\n\nuse the\nbalance\nboosters\nslider.");
            firstRunOfEvent = false;
            routineReference = StartCoroutine(BlinkObject(balanceGuide, 2.5f));
        }

        if(!consoleScreen.isWriting && Mathf.Abs(tiltSlider.transform.localPosition.x) > 0.001f )
        {   
            StopCoroutine(routineReference);
            balanceGuide.SetActive(false);

            progressPaused = false;
            return true;
        }
        return false;
    }

    bool EventAsteroidIntro()
    {
        if(firstRunOfEvent)
        {
            consoleScreen.Clear();
            consoleScreen.StartMessage("Objects\ndetected\nin path!\n\nusing\nweapons\nsystems\nadvised!");
            firstRunOfEvent = false;
            routineReference = StartCoroutine(BlinkObject(switchGuide, 2.5f));
        }

        if(!consoleScreen.isWriting && EvaluateConsoleSwitched())
        {
            StopCoroutine(routineReference);
            switchGuide.SetActive(false);
            StartCoroutine(BlinkObject(asteroidGuide, 0f));

            asteroidSpawner.SpawnAsteroid();
            asteroidSpawner.SpawnAsteroid();

            return true;
        }
        return false;
    }

    bool asteroidsExtraOnce = true;

    bool EventAsteroidsExtra()
    {
        if(firstRunOfEvent)
        {
            consoleScreen.Clear();
            consoleScreen.StartMessage("Objects\ndetected\nin path!\n\nusing\nweapons\nsystems\nadvised!");
            firstRunOfEvent = false;
        }

        if(!consoleScreen.isWriting)
        {
            if(asteroidsExtraOnce == true)
            {
                asteroidSpawner.SpawnAsteroid();
                asteroidSpawner.SpawnAsteroid();
                asteroidSpawner.SpawnAsteroid();
                asteroidsExtraOnce = false;
            }

            return true;
        }
        return false;
    }

    bool asteroidsExtra2Once = true;

    bool EventAsteroidsExtra2()
    {
        if(firstRunOfEvent)
        {
            consoleScreen.Clear();
            consoleScreen.StartMessage("Last bunch\nof rocks\nbefore core!\n\nusing\nweapons\nsystems\nadvised!");
            firstRunOfEvent = false;
        }

        if(!consoleScreen.isWriting)
        {
            if(asteroidsExtra2Once == true)
            {
                asteroidSpawner.SpawnAsteroid();
                asteroidSpawner.SpawnAsteroid();
                asteroidSpawner.SpawnAsteroid();
                asteroidSpawner.SpawnAsteroid();
                asteroidsExtra2Once = false;
            }

            return true;
        }
        return false;
    }

    bool EventDroneIntro1()
    {
        if(firstRunOfEvent)
        {
            consoleScreen.Clear();
            consoleScreen.StartMessage("external\noxygen tank\ndamaged\n\n< deploy\n  repair\n  drone");
            firstRunOfEvent = false;
        }

        if(!consoleScreen.isWriting && EvaluateTopButton())
        {
            return true;        
        }
        return false;
    }

    bool EventDroneIntro2()
    {
        if(firstRunOfEvent)
        {
            consoleScreen.Clear();
            consoleScreen.StartMessage("drone ai\nrequires\nupdate\n\n< start\n  update\n< start\n  manual\n  control");
            firstRunOfEvent = false;
        }

        if(!consoleScreen.isWriting)
        {
            if(EvaluateTopButton())
            {
                consoleScreen.Clear();
                consoleScreen.StartMessage("updating@.@.@.\nupdate\nfailed, no 8g\nbeacons in\nrange\n\n< start\n  manual\n  control");
            }

            if(EvaluateBottomButton())
            {
                return true;
            }
        }
        return false;
    }

    bool EventDroneIntro3()
    {
        if(firstRunOfEvent)
        {
            consoleScreen.Clear();
            droneMinigame.EnableHoleA();
            droneMinigame.StartDroneGame();
            StartCoroutine(BlinkObject(droneGuide, 0f));
            
            firstRunOfEvent = false;
        }

        if(droneMinigame.CheckIfCanEnd())
        {
            droneMinigame.EndDroneGame();
            return true;
        }

        return false;
    }

    bool EventDroneIntro3NoTutorial()
    {
        if(firstRunOfEvent)
        {
            consoleScreen.Clear();
            droneMinigame.EnableHoleB();
            droneMinigame.StartDroneGame();
            
            firstRunOfEvent = false;
        }

        if(droneMinigame.CheckIfCanEnd())
        {
            droneMinigame.EndDroneGame();
            return true;
        }

        return false;
    }

    bool EventEnding1()
    {
        if(firstRunOfEvent)
        {
            progressPaused = true;
            droneMinigame.EndDroneGame();
            shipAngle.enabled = false;
            if(consoleSwitch.switchState == SwitchState.Weapons)
            {
                consoleSwitch.switchState = SwitchState.Console;
                consoleSwitch.ChangeState();
            }

            consoleScreen.Clear();
            firstRunOfEvent = false;
            consoleScreen.StartMessage("incoming\nsignal@@.@@.@@.\n\n+--*)/+*-\n+)!)*/why.,'../\n/;did..,,they.,!\nSEND..-/.*-.!-\nanother?.,/@@@@@@@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            return true;
        }
        return false;
    }

    bool EventEnding2()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("incoming\nsignal@@.@@.@@.\n\nyou are not\nwanted here.\nyou are an\nexample.\nyou are@@@@@@n't.@@@@@@@@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            return true;
        }
        return false;
    }

    bool EventEnding3()
    {
        if(firstRunOfEvent)
        {
            endingWitnessed = true;
            firstRunOfEvent = false;
            consoleScreen.StartMessage("Object\ndetected\nin path!\n\nusing\nweapons\nsystems\nadvised!");
            for(int i = 0; i < 40; i++)
            {
                asteroidSpawner.SpawnAsteroid();
            }
        }
        return false;
    }

    bool deathMessageDone = false;

    bool EventDeath()
    {
        if(firstRunOfEvent)
        {
            progressPaused = true;
            droneMinigame.EndDroneGame();
            shipAngle.enabled = false;
            if(consoleSwitch.switchState == SwitchState.Weapons)
            {
                consoleSwitch.switchState = SwitchState.Console;
                consoleSwitch.ChangeState();
            }

            consoleScreen.Clear();
            if(endingWitnessed)
            {
                consoleScreen.StartMessage("hull status\ncritical@.@.@.\npilot status\ncritical@.@.@.\ninteraction\nwith entity\nrecorded.\nmission\nsuccessful.@@@@@");
            }
            else
            {
                consoleScreen.StartMessage("hull status\ncritical@.@.@.\npilot status\ncritical@.@.@.\n\nmission\nfailed@@@@@@@@@");
            }

            firstRunOfEvent = false;
        }

        deathOverlay.color = new Color(deathOverlay.color.r, deathOverlay.color.g, deathOverlay.color.b, Mathf.Sin(Time.time) * 0.15f + 0.65f);

        if(!consoleScreen.isWriting)
        {
            if(!deathMessageDone)
            {
                deathMessageDone = true;
                if(!endingWitnessed)
                {
                    consoleScreen.StartMessage("we are very\ndisappointed\n\n\n< press\n  button to\n  restart");
                }
                else
                {
                    consoleScreen.StartMessage("we are very\ndelighted\n\n\nthank you\nfor playing!");
                }
            }
            else if(EvaluateTopButton())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        return false;
    }

    bool EventQuiz1()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("start\nmandatory\npilot quiz?\n\n< yes\n\n< of course");
        }

        if(!consoleScreen.isWriting && (EvaluateTopButton() || EvaluateBottomButton()))
        {
            return true;
        }

        return false;
    }

    bool EventQuiz2()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("which of\nthese better\ndescribes\nyou?\n< courageus\n  and smart\n< lucky and\n  unqualified");
        }

        if(!consoleScreen.isWriting && (EvaluateTopButton() || EvaluateBottomButton()))
        {
            return true;
        }

        return false;
    }

    bool EventQuiz3()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("we want to\nremind you\nthat lying\non the\nmandatory\npilot quiz\nwill result\nin immediate\ntermination@@@@@@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            return true;
        }

        return false;
    }

    bool EventQuiz4()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("will this\nquest grant\nyou fame or\nrespect?\n< i believe\n  so\n< i am not\n  sure");
        }

        if(!consoleScreen.isWriting &&EvaluateTopButton())
        {
            consoleScreen.StartMessage("okay now\nyou're lying\nto yourself\ntoo.@@@@@\nmandatory\npilot quiz\nhas been\ncompleted.");
            return true;
        }
        if(!consoleScreen.isWriting &&EvaluateBottomButton()){
            consoleScreen.StartMessage("honesly,\nme neither\n\n\nmandatory\npilot quiz\nhas been\ncompleted.");
            return true;
        }

        return false;
    }

    bool EventJokes1()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("after such a\nstressful\nsituation\nprotocol\nrequires me\nto provide 3\njokes for\nstress relief@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            return true;
        }

        return false;
    }

    bool EventJokes2()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("jupiter's\ngreat red\nspot is\nshrinking.@@@@@@\nthanks to\nthe\nprescribed\ntreatment.@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            return true;
        }

        return false;
    }

    bool EventJokes3()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("jupiter is a\ngas giant@@.@@.@@.\n\ndamn bean\nburritos\nman...@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            return true;
        }

        return false;
    }

     bool EventJokes4()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("why did the\nchicken\ncross the\nroad?@@@@\nto avoid\nbeing\nobliterated\nby the rock.@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            return true;
        }

        return false;
    }

    bool joke5Once = true;
     bool EventJokes5()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("what i'm\ntrying to\nsay is that\nthere's some\nmore\nmeteors\ncoming your\nway.@@@@@@@");
        }

        if(!consoleScreen.isWriting)
        {
            if(joke5Once)
            {
                for(int i = 0; i < 4; i++)
                {
                    asteroidSpawner.SpawnAsteroid();
                }
                joke5Once = false;    
            }
            return true;
        }

        return false;
    }

    bool EventStrongWinds()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("strong\njupiter\nwinds\ndetected.\n\nmake sure to\nkeep the\nlander\nbalanced.");
        }

        if(!consoleScreen.isWriting)
        {
            shipAngle.noiseStrength = 1f;
            return true;
        }

        return false;
    }

    bool EventStrongWindsEnd()
    {
        if(firstRunOfEvent)
        {
            firstRunOfEvent = false;
            consoleScreen.StartMessage("strong\njupiter\nwinds have\npassed.\n\ngood job@@@?@");
        }

        if(!consoleScreen.isWriting)
        {
            shipAngle.noiseStrength = 0.2f;
            return true;
        }

        return false;
    }

    IEnumerator BlinkObject(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(true);
        for(int i = 0; i < 4; i++)
        {
            go.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            go.SetActive(true);
            yield return new WaitForSeconds(0.7f);
        }
        go.SetActive(false);
    }

}
