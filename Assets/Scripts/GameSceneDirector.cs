using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameSceneDirector : MonoBehaviour
{
    // �^�C���}�b�v
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    // �}�b�v�S�̍��W

    public Vector2 TileMapStart;
    public Vector2 TileMapEnd;
    public Vector2 WorldStart;
    public Vector2 WorldEnd;


    // EnemyContller�ŌĂяo��������
    // GameSceneDirector����ăv���C���[�̃I�u�W�F�N�g�ɃA�N�Z�X�ł���悤�ɂ���


    public PlayerController Player;
    
    [SerializeField] Transform parentTextDamage;
    [SerializeField] GameObject prefabTextDamage;

    // �^�C�}�[
    [SerializeField] Text textTimer;
    public float GameTimer;
    public float OldSeconds;


    private void Start()
    {
        // �����ݒ�
        OldSeconds = -1;


        //�@�J�����̈ړ��ł���͈�

        foreach (Transform item in grid.GetComponentInChildren<Transform>())
        {
            // �q���̃|�W�V����������o���āA
            // �J�n�ʒu�i��ԍ���[�̈ʒu������o���j
            if (TileMapStart.x > item.position.x)
            {
                TileMapStart.x = item.position.x;
            }

            if (TileMapStart.y > item.position.y)
            {
                TileMapStart.y = item.position.y;
            }

            // �I���ʒu�i��ԉE��[�̈ʒu������o���j
            if (TileMapEnd.x < item.position.x)
            {
                TileMapEnd.x = item.position.x;
            }

            if (TileMapEnd.y < item.position.y)
            {
                TileMapEnd.y = item.position.y;
            }

            // ��ʏc�����̕`��͈́i�f�t�H���g�łT�^�C���j
            float cameraSize = Camera.main.orthographicSize;

            // ��ʏc����i16�F9�z��j 
            float aspect = (float)Screen.width / (float)Screen.height;

            // �v���C���[�̈ړ��ł���͈́i�A�X�y�N�g�䗦�ƃJ�����̃T�C�Y���擾���āAWorldStart�F�����[�@WorldEnd�F�E��[���擾����j
            // 
            WorldStart = new Vector2(TileMapStart.x - cameraSize * aspect, TileMapStart.y - cameraSize);
            WorldEnd = new Vector2(TileMapEnd.x + cameraSize * aspect, TileMapEnd.y + cameraSize);

        }


    }

    private void Update()
    {
        // �Q�[���^�C�}�[�X�V
        UpdateGameTimer();
    }

    // �v���n�u����TextDamageController�ō�����R���g���[���̊֐����Ăяo��
    // �_���[�W�\��
    public void DispDamege(GameObject target, float damege)
    {
        GameObject obj = Instantiate(prefabTextDamage, parentTextDamage);
        obj.GetComponent<TextDamageController>().Init(target, damege);
    }

    //�@�Q�[���^�C�}�[
    void UpdateGameTimer()
    {
        GameTimer += Time.deltaTime;

        // �O��ƕb���������Ȃ珈�������Ȃ�
        int seconds = (int)GameTimer % 60;
        if (seconds == OldSeconds) return;

        textTimer.text = Units.GetTextTimer(GameTimer);
        OldSeconds = seconds;



    }
}
