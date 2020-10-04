using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace engine
{
    public class DelegrateMgr : Globals
    {
        // Variables declarations 

        public bool isOnRoute;
        public bool isGameOver;
        public bool isMenuOn;
        public bool isNavPaused;
        public bool isGunLoaded;
        public bool isReloading;
        public bool isInUse = false;
        public float swingRate = 0.5f;
        public bool isInventoryUIOn;
        public Transform myTarget;
        public Text missionOutcomeText;
        public GameObject missionUIPanel;
        public GameObject objectiveUIPanel;
        public string missionAccomplished = "Mission Accomplished!";
        public string animationBoolPursuing = "isPursuing";
        public string animationTriggerStruck = "Struck";
        public string animationTriggerMelee = "Attack";
        public string animationTriggerRecovered = "Recovered";
        public bool isVehicleOccupied;
        public string defaultTag = "Untagged";
        public string exitButton = "PickUpItem";
        public int defaultLayerNumber;
        public LayerMask defaultLayer;
        public GameObject cameraMultipurpose;
        public Transform cabin;
        public GameObject vehicleCamera;
        public Transform cameraTarget;
        [HideInInspector]
        public GameObject driver;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                 GENERAL EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void GeneralEventHandle();

        public event GeneralEventHandle EventMenuToggle;
        public event GeneralEventHandle EventInventoryUIToggle;
        public event GeneralEventHandle EventRestartLevel;
        public event GeneralEventHandle EventGoToMenuScene;
        public event GeneralEventHandle EventGameOver;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                 PLAYER EVENTS                           ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void PlayerEventHandle();
        public event PlayerEventHandle EventInventoryChanged;
        public event PlayerEventHandle EventHandsEmpty;
        public event PlayerEventHandle EventAmmoChanged;

        public delegate void PlayerHealthEventHandle(int healthChange);
        public event PlayerHealthEventHandle EventPlayerHealthDecrease;
        public event PlayerHealthEventHandle EventPlayerHealthIncrease;

        public delegate void AmmoPickupEventHandle(string ammoName, int quantity);
        public event AmmoPickupEventHandle EventPickedUpAmmo;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                 ENEMY EVENTS                            ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void EnemyEventHandle();
        public event EnemyEventHandle EventEnemyDied;
        public event EnemyEventHandle EventEnemyWalking;
        public event EnemyEventHandle EventEnemyReachedNavTargetPos;
        public event EnemyEventHandle EventEnemyAttack;
        public event EnemyEventHandle EventEnemyTargetLost;
        public event EnemyEventHandle EventEnemyHealthLow;
        public event EnemyEventHandle EventEnemyHealthHigh;

        public delegate void HealthEventHandle(int health);
        public event HealthEventHandle EventEnemyHealthDecrease;
        public event HealthEventHandle EventEnemyIncreaseHealth;

        public delegate void NavTargetEventHandle(Transform targetTransform);
        public event NavTargetEventHandle EventEnemySetNavTarget;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///               DESTRUCTION EVENTS                        ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void HealthEventDeduct(int health);
        public event HealthEventDeduct EventDeductHealth;

        public delegate void DestructEventHandle();
        public event DestructEventHandle EventDestroyTarget;
        public event DestructEventHandle EventHealthLow;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                     GUN EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void GunEventHandle();
        public event GunEventHandle EventPlayerInput;
        public event GunEventHandle EventGunUnusable;
        public event GunEventHandle EventRequestReload;
        public event GunEventHandle EventRequestResetGun;
        public event GunEventHandle EventToggleBurstFire;

        public delegate void GunHitEventHandle(RaycastHit hitPosition, Transform hitTransform);
        public event GunHitEventHandle EventShotDefault;
        public event GunHitEventHandle EventShotEnemy;

        public delegate void GunAmmoEventHandle(int currentAmmo, int carriedAmmo);
        public event GunAmmoEventHandle EventAmmoChange;

        public delegate void GunCrosshairEventHandle(float speed);
        public event GunCrosshairEventHandle EventSpeedCaptured;

        public delegate void GunNpcEventHandle(float rnd);
        public event GunNpcEventHandle EventNpcInput;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                    ITEM EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        private bool isOnPlayer;

        public delegate void ObjectEventHandle();
        public event ObjectEventHandle EventObjectThrow;
        public event ObjectEventHandle EventObjectPickup;

        public delegate void PickupEventHandle(Transform item);
        public event PickupEventHandle EventPickupAction;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                   MELEE EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void MeleeEventHandle();
        public event MeleeEventHandle EventPlayerMeleeInput;
        public event MeleeEventHandle EventMeleeReset;

        public delegate void MeleeHitEventHandle(Collision hitCollision, Transform hitTransform);
        public event MeleeHitEventHandle EventHit;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                 MISSION EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void MissionEventHandle();
        public event MissionEventHandle EventObjectiveComplete;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                     NPC EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void NPCEventHandle();
        public event NPCEventHandle EventNpcDie;
        public event NPCEventHandle EventNpcLowHealth;
        public event NPCEventHandle EventNpcHealthRecovered;
        public event NPCEventHandle EventNpcWalkAnim;
        public event NPCEventHandle EventNpcStruckAnim;
        public event NPCEventHandle EventNpcAttackAnim;
        public event NPCEventHandle EventNpcRecoveredAnim;
        public event NPCEventHandle EventNpcIdleAnim;

        public delegate void NPCHealthEventHandle(int health);
        public event NPCHealthEventHandle EventNpcDeductHealth;
        public event NPCHealthEventHandle EventNpcIncreaseHealth;

        public delegate void NPCRelationsChangeEventHandle();
        public event NPCRelationsChangeEventHandle EventNPCRelationsChange;


        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                 VEHICLE EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////

        public delegate void ExitEventHandle();
        public event ExitEventHandle EventExitVehicle;

        public delegate void VehicleEventHandle(GameObject driverGO, string driverTag, LayerMask driverLayer);
        public event VehicleEventHandle EventEnterVehicle;

        ///////////////////////////////////////////////////////////////
        ///                                                         ///
        ///                  CAMERA EVENTS                          ///
        ///                                                         ///
        ///////////////////////////////////////////////////////////////


        public delegate void CameraTargetEventHandle(Transform targetTransform);
        public event CameraTargetEventHandle EventAssignCameraTarget;
    }

}