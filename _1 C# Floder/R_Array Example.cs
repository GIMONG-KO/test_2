
using System.Linq;
using UnityEngine;

public class ArrayExamples : MonoBehaviour
{
    #region Values

    
    // 데이터 관리를 용이하게 하기 위해 
    // 상수화 시킨 숫자를 쓴다.
    private const int ARRAY_SIZE = 10;   
    
    // 플레이어 점수를 저장하는 배열
    private int[] playerScores = new int[ARRAY_SIZE];

    // 아이템 이름을 저장하는 배열
    private string[] itemNames = { "검", "방패", "포션", "활", "마법서" };

    // 적 프리팹을 저장하는 배열
    public GameObject[] enemyPrefabs;

    // 맵의 타일 타입을 저장하는 2D 배열
    private int[,] mapTiles = new int[10, 10];

    public GameObject Cube;
    
    public GameObject Sphere;
    
    private GameObject[,] CubeTiles = new GameObject[10, 10];

    #endregion
    void Start()
    {
        // PlayerScoresExample();
        // ItemInventoryExample();
        // EnemySpawnExample();
        MapGenerationExample();
    }
    
    void PlayerScoresExample()
    {
        // 플레이어 점수 할당
        for (int i = 0; i < playerScores.Length; i++)
        {
            // 100 ~ (1000 - 1) 사이의 랜덤한 값을 할당한다.
            // Random에는 System의 Random이 있고
            // Unity의 Random이 있는데
            // 우리는 유니티 Random을 활용한다.
            playerScores[i] = Random.Range(100, 1000);
        }

        #region MaxValue찾기

        // 방법 1
        
        int maxValue = 0;
        for (var i = 0; i < playerScores.Length; i++)
        {
            // maxValue보다 playerScores[i]의 값이 크다면
            if (playerScores[i] > maxValue)
            {
                // 제일 큰 값은 playerScores[i]으로 갱신된다. 
                maxValue = playerScores[i];   
            }
        }
        
        Debug.Log($"최고 점수1: {maxValue}");

        // 방법 2
        
        // 위의 MaxValue 찾기를 Linq를 이용해 C#에 미리 구현된 기능을 사용해서 간단히 표현할수 있다.
        // 최고 점수 찾기
        int highestScore = playerScores.Max();
        Debug.Log($"최고 점수2: {highestScore}");
        
        #endregion

        #region Average 계산하기
        
        // 방법 1
        int totalValue = 0;
        float averageValue = 0;
        
        // 반복문을 돌면서 playerScores를 모두 더한다.
        for (var i = 0; i < playerScores.Length; i++)
        {
            totalValue += playerScores[i];
        }
        
        // 정수 / 정수는 정수밖에 안나오므로 정수 / 실수를 해서 실수계산이 이뤄지도록 한다.
        averageValue = totalValue / (float)playerScores.Length;
        Debug.Log($"평균 점수1: {averageValue:F2}");
        
        // 방법 2
        
        // 평균 점수 계산
        double averageScore = playerScores.Average();
        Debug.Log($"평균 점수2: {averageScore:F2}");
        
        #endregion
    }
    
    void ItemInventoryExample()
    {
        // 랜덤 아이템 선택
        int randomIndex = Random.Range(0, itemNames.Length);
        string selectedItem = itemNames[randomIndex];
        Debug.Log($"선택된 아이템: {selectedItem}");

        string itemName = "포션";
        
        // array에서 순차적으로 요소 값을 찾을 때 linq의 Contains를 사용하는데
        // Contatins를 직접 구현한 내용인다.
        
        // Contains 직접 구현
        bool hasPotion1 = Contains(itemName);
        Debug.Log($"포션 보유 여부: {hasPotion1}");

        // 특정 아이템 검색
        string searchItem = "방패";
        bool hasPotion2 = itemNames.Contains(searchItem);
        Debug.Log($"포션 보유 여부: {hasPotion2}");
    }

    // 접근 제어자 (public, protected, private, internal) / 반환타입 함수 / 이름 / 매개 변수
    
    // 배열의 요소들에 순차적으로 접근해서 매개변수의 값과 일치하는 요소가 있는지 체크하여 반환한다.
    private bool Contains(string itemName)
    {
        // Array의 Contains을 구현하는 방법
        for (var i = 0; i < itemNames.Length; i++)
        {
            if (itemNames[i] == itemName)
            {
                return true;
            }
        }

        return false;
    }
    
    // 랜덤한 포지션에 enemyPrefabs의 요소중에 랜덤한 프리팹을 골라 생성하는 코드
    void EnemySpawnExample()
    {
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            // 랜덤 위치에 랜덤 적 생성
            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);
            Debug.Log($"적 생성됨: {enemyPrefabs[randomEnemyIndex].name}");
        }
        else
        {
            Debug.LogWarning("적 프리팹이 할당되지 않았습니다.");
        }
    }
    
    // 2차원 배열을 사용하여 타일을 표현하고 타일의 요소에 따라서 박스와 스피어로 표현한 함수
    void MapGenerationExample()
    {
        // 간단한 맵 생성 (0: 빈 공간, 1: 벽)
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                // Random의 value는 0 ~ 1사이의 값이 반환되는데
                // 삼항 연산자를 활용하여 20%의 확률로 1이 80%의 확률로 0이 tiles에 저장되도록 한다.
                mapTiles[x, y] = Random.value > 0.8f ? 1 : 0;
            }
        }

        // 맵 출력
        // string mapString = "생성된 맵:\n";
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                // 방법 1. 삼항 연산자를 사용하고 그 객체를 관리하기 위해 배열에 넣기
                //CubeTiles[x,y] = mapTiles[x, y] == 1 ? Instantiate(Cube) : null;
                
                // 방법 2. 조건문을 통해 그 객체를 관리하기 위해 배열에 넣기
                if (mapTiles[x, y] == 1)
                {
                    CubeTiles[x, y] = Instantiate(Cube, new Vector3(x - 5, y - 5, 0), Quaternion.identity);
                }
                else
                {
                    CubeTiles[x, y] = Instantiate(Sphere, new Vector3(x - 5, y - 5, 0), Quaternion.identity);
                }
                
            }
        }
    }
}

