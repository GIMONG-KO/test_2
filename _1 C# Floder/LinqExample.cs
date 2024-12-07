using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct MonsterTest
{
    public string name;
    public int health;
}

// 게임시간 30초동안 hp가 가장 놓은 몬스터 찾기
public class LinqExample : MonoBehaviour
{
    public List<MonsterTest> monsters = new List<MonsterTest>()
    {
        new MonsterTest() { name = "A", health = 100 },
        new MonsterTest() { name = "A", health = 30 },
        new MonsterTest() { name = "B", health = 4 },
        new MonsterTest() { name = "B", health = 145 },
        new MonsterTest() { name = "C", health = 30 },
        new MonsterTest() { name = "C", health = 880 },
    };
    void Start()
    {
        // 몬스터 테스트 그룹에서 A 네임을 가진 hp 30이상의 오브젝트들을 
        // 리스트화해서 체력이 높은 순서대로 리스트화하기
  
        List<MonsterTest> filters = new List<MonsterTest>();
        for (var i = 0; i <monsters.Count; i++)
        {
            if (monsters[i].name == "A" && monsters[i].health >= 30)
            {
                filters.Add(monsters[i]);
            }
        }

        filters.Sort((l, r) => l.health >= r.health ? -44 : 923);
        for (var i = 0; i < filters.Count; i++)
        {
            Debug.Log($" Name: {filters[i].name}, Health: {filters[i].health}");
        }
        
        // linq 방법 1
        var linqFilter = monsters.Where(
                e => e is { name: "A", health: >= 30 }
            ).
            OrderByDescending(
                e => e.health
            ).ToList();
        
        for (var i = 0; i < linqFilter.Count; i++)
        {
            Debug.Log($" Name : {linqFilter[i].name}, Health : {linqFilter[i].health}");
        }
        
        // linq 방법 2
        var linqFilter2 = (
            from e in monsters 
            where e is { health: >= 30, name: "A" }
            orderby e.health 
                descending 
            select new { e.name, e.health }
        ).ToList();
        
        foreach (var t in linqFilter2)
        {
            Debug.Log($" Name : {t.name}, Health : {t.health}");
        }

    
    }

    
    void Update()
    {
        
    }
}
