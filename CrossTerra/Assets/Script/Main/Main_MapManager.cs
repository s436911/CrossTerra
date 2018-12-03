using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Main_MapManager : MonoBehaviour {
    public static Vector2 Lng = Vector2.zero;
    public static Dictionary<Vector2, Block> mapInfo = new Dictionary<Vector2, Block>();
	public static Block blockNow {
		get { return mapInfo[Lng];}
	}

	public GameObject mainBlock;
	public GameObject subBlock;
	public float blockShift = 0.91f;
	public float heightShift = 0.1f;
	public int padSize = 5;
	public int blocksSzie = 50;
	public static float genInput = 0.03f;

	public Transform groundSet;

	public Text coordinatesText;
    public Text regionText;
    public Text terrianText;
	public Text buildingText;

	private Dictionary<Vector2, BlockPad>		crossBlockpPad			= new Dictionary<Vector2, BlockPad>();
	private Dictionary<Vector2, SpriteRenderer> crossRegion				= new Dictionary<Vector2, SpriteRenderer>();
	private Dictionary<Vector2, SpriteRenderer> crossTerrain			= new Dictionary<Vector2, SpriteRenderer>();
	private Dictionary<Vector2, SpriteRenderer> crossBuilding			= new Dictionary<Vector2, SpriteRenderer>();
	private Dictionary<Vector2, Animator>		crossGroundBack			= new Dictionary<Vector2, Animator>();
	private Dictionary<Vector2, Animator>		crossGroundBackLight	= new Dictionary<Vector2, Animator>();

	private Dictionary<Vector2, bool> blocks = new Dictionary<Vector2, bool>();
	private List<Area> areas = new List<Area>();
	
	public float inputGape = 0.5f;
	private float inputClock;

	public bool areaMode = true;
	public bool sizeOptimize = false;
	public int minOptimizeNum = 16;
	public int maxOptimizeNum = 128;
	public float SgenInput = 0.03f;
	
	private void NewBlock(Vector2 realLng, bool isMain = false , int direction = 0) {
		Vector2 uiLng = new Vector2(realLng.y + realLng.x, realLng.y - realLng.x);
		int sortOrder = (-(int)uiLng.y + 2) * 10;
		GameObject newObj = Instantiate(isMain ? mainBlock : subBlock);
		SpriteRenderer tempSp;
		BlockPad tempBlockPad;

		newObj.transform.SetParent(groundSet);
		newObj.transform.localPosition = new Vector3(uiLng.x * blockShift * 2, uiLng.y * blockShift, 0);
		newObj.name = realLng.ToString();
		
		tempSp = newObj.GetComponent<SpriteRenderer>();
		tempSp.sortingOrder += sortOrder;
		crossRegion.Add(realLng, tempSp);
		
		tempBlockPad = newObj.GetComponent<BlockPad>();
		tempBlockPad.blockPos = new Vector3(realLng.x, realLng.y , uiLng.y);
		tempBlockPad.direction = direction;
		crossBlockpPad.Add(realLng, tempBlockPad);

		foreach (Transform child in newObj.transform) {
			if (child.name == "TERRIAN") {
				tempSp = child.GetComponent<SpriteRenderer>();
				tempSp.sortingOrder += sortOrder;
				crossTerrain.Add(realLng, tempSp);

			} else if (child.name == "BUILDING") {
				tempSp = child.GetComponent<SpriteRenderer>();
				tempSp.sortingOrder += sortOrder;
				crossBuilding.Add(realLng, tempSp);

			} else if (child.name == "GROUNDBG") {
				tempSp = child.GetComponent<SpriteRenderer>();
				tempSp.sortingOrder += sortOrder;
				crossGroundBack.Add(realLng, child.GetComponent<Animator>());

			} else if (child.name == "GROUNDBGL") {
				tempSp = child.GetComponent<SpriteRenderer>();
				tempSp.sortingOrder += sortOrder;
				crossGroundBackLight.Add(realLng, child.GetComponent<Animator>());
			}
		}

		/*
		Block block = new Block(realLng, Database.regionBase[0]);
		mapInfo.Add(block.location, block);
		if (realLng == Vector2.zero) {
			block.Build(Database.buildingBase[0]);
		} */
	}

	private void Init() {
		Lng = Vector2.zero;
        mapInfo = new Dictionary<Vector2, Block>();
		Database.LoadDatabase();
		genInput = SgenInput;

		//MainPad生成
		NewBlock(Vector2.zero, true);
		NewBlock(Vector2.right, true , 1);
		NewBlock(Vector2.down, true , 2);
		NewBlock(Vector2.left, true , 3);
		NewBlock(Vector2.up, true , 4);

		//SubPad生成
		int spawnSzie = padSize;

		for (int px = -spawnSzie; px <= spawnSzie; px++) {
			for (int py = -spawnSzie; py <= spawnSzie; py++) {
				int nowRound = Mathf.Abs(px) + Mathf.Abs(py);

				if (nowRound <= spawnSzie && nowRound > 1) {
					NewBlock(new Vector2(px, py));
				}
			}
		}

		spawnSzie = 1;
		
		//基礎生成
		GenMap(new Vector2(0, 0));

		/*
		//主城生成
		Area home = new Area(Database.biomeBase[0]);
		ReasignArea(mapInfo[new Vector2(0, 0)].GetArea(), home);
		ReasignArea(mapInfo[new Vector2(0, -1)].GetArea(), home);
		ReasignArea(mapInfo[new Vector2(-1, 0)].GetArea(), home);
		ReasignArea(mapInfo[new Vector2(-1, -1)].GetArea(), home);*/

		//刷新
		ShowMap();
	}

    void Awake() {
		Init();
		ShowMap();
		ShowBlockInfo();
    }

	void Update() {
		if (Input.GetKey(KeyCode.RightArrow)) {
			Move(1);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			Move(2);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			Move(3);
		}		
		if (Input.GetKey(KeyCode.UpArrow)) {
			Move(4);
		}
	}

	public void Move(BlockPad valuePad) {  
		Move(valuePad.direction);
	} 

	public void Move(int direction) { //0~3 EWSN    
		if (Mathf.Abs(Time.timeSinceLevelLoad - inputClock) > inputGape) {
			if (direction == 1) {
				Lng.x++;

			} else if (direction == 2) {
				Lng.y--;

			} else if (direction == 3) {
				Lng.x--;

			} else if (direction == 4) {
				Lng.y++;

			} else {
				return;
			}

			ShowMap();
			CountPos();			
			inputClock = Time.timeSinceLevelLoad;
		}
    }
		
    public void CountPos() {
		//******************顯示該座標
		ShowBlockInfo();

		//******************消耗/回復體力
		if (blockNow.building != null) {
			if (blockNow.building.sID == 0) {
				Main_PlayerManager.direct.Supply(30);
			}
			if (blockNow.building.sID == 1) {
				Main_PlayerManager.direct.Supply(30);
			}
			if (blockNow.building.sID == 2) {
				Main_PlayerManager.direct.Supply(30);
			}
		} else { 
			Main_PlayerManager.direct.Eat(mapInfo[Lng].GetSupCost());
		}
	}

	//刷新地圖UI
	public void ShowMap() {
		foreach (Vector2 keyLng in crossRegion.Keys) {
			//Debug.LogError(Lng + keyLng + "/" + !blocks.ContainsKey(GetBlocksLng(Lng + keyLng)) + "/" + !blocks[GetBlocksLng(Lng + keyLng)]);

			if (!blocks.ContainsKey(GetBlocksLng(Lng + keyLng)) || !blocks[GetBlocksLng(Lng + keyLng)]) {				
				GenMap(Lng + keyLng);

			} else {
				
				ShowBlock(keyLng, Lng + keyLng);
			}
		}
	}

	public Vector2 GetBlocksLng(Vector2 p) {
		if (p.x >= 0) {
			p.x = (int)(p.x / blocksSzie);
		} else {
			p.x = (int)((p.x + 1) / blocksSzie) - 1;
		}
		
		if (p.y >= 0) {
			p.y = (int)(p.y / blocksSzie);
		} else {
			p.y = (int)((p.y + 1) / blocksSzie) - 1;
		}
		
		return p;
	}

	//生成區域
	public void GenMap(Vector2 bp) {
		bp = GetBlocksLng(bp);

		if (!blocks.ContainsKey(bp) || blocks[bp] == false) {
			Debug.Log("MainMap:" + bp + " X:" + (int)(bp.x * blocksSzie) + "<" + (int)((bp.x + 1) * blocksSzie) + " Y:" + (int)(bp.y * blocksSzie) + "<" + (int)((bp.y + 1) * blocksSzie));
			GenBlocks(bp);
			GenBlocks(bp + Vector2.up);
			GenBlocks(bp + Vector2.left);
			GenBlocks(bp + Vector2.right);
			GenBlocks(bp + Vector2.down);

			DFSWBlock(new Vector2(bp.x * blocksSzie, (bp.x + 1) * blocksSzie), new Vector2(bp.y * blocksSzie, (bp.y + 1) * blocksSzie));
			blocks[bp] = true;
		} else {
			Debug.Log("MainMap(skip):" + bp);
		}
	}

	public void GenBlocks(Vector2 bp) {
		if (!blocks.ContainsKey(bp)) {
			Debug.Log("SubMap:" + bp + " X:" + (int)(bp.x * blocksSzie) + "<" + (int)((bp.x + 1) * blocksSzie) + " Y:" + (int)(bp.y * blocksSzie) + "<" + (int)((bp.y + 1) * blocksSzie));
			for (int px = (int)(bp.x * blocksSzie); px < (int)((bp.x + 1) * blocksSzie); px++) {
				for (int py = (int)(bp.y * blocksSzie); py < (int)((bp.y + 1) * blocksSzie); py++) {
					SpawnPos(new Vector2(px, py));
				}
			} 

			blocks.Add(bp, false);
		} else {

		}
	}

	public void ShowBlock(Vector2 padLng, Vector2 tagetLng) {
		BlockPad showing = crossBlockpPad[padLng];
		Block value = mapInfo[tagetLng];

		bool isMain = (Mathf.Abs(tagetLng.x) + Mathf.Abs(tagetLng.y) > 1) ? false : true;
		
		Color showColor = value.GetArea().biome.color * (isMain ? 1 : 1);	//邊緣調色
		showColor.a = 1;

		showing.transform.localPosition = new Vector2(showing.transform.localPosition.x , showing.blockPos.z * blockShift + heightShift * value.GetHeight());
		crossRegion[padLng].color = showColor;
        crossTerrain[padLng].color = showColor;
		
        if (value.building != null ) {
			crossBuilding[padLng].sprite = value.building.texture;

			crossGroundBack[padLng].gameObject.SetActive(true);
			crossGroundBack[padLng].Play("Ground" + value.building.rarity);
			
			crossGroundBackLight[padLng].gameObject.SetActive(true);
            crossGroundBackLight[padLng].Play("Ground" + value.building.rarity);

        } else {			
			crossBuilding[padLng].sprite = null;
			crossGroundBack[padLng].gameObject.SetActive(false);
			crossGroundBackLight[padLng].gameObject.SetActive(false);

		}
		crossTerrain[padLng].sprite = value.cterrain.texture;
	}
    
	public Block SpawnPos(Vector2 location) {
		if (mapInfo.ContainsKey(location)) {	//讀取舊有方格資訊
			mapInfo[location].ReBuild();
			return mapInfo[location];

		} else {
			Block block = new Block(location);
			mapInfo.Add(block.location, block);
			return block;
		}
	}
	
	public void ShowBlockInfo(BlockPad valuePad) {
		ShowBlockInfo(valuePad.blockPos);
	}

	public void ShowBlockInfo() {
		ShowBlockInfo(Vector2.zero);
	}

	public void ShowBlockInfo(Vector2 valuePos) {
		//******************顯示該座標
		Vector2 showLng = valuePos + Lng;

		terrianText.text = mapInfo[showLng].cterrain.sName;
		coordinatesText.text = "X:" + showLng.x.ToString() + "  Y:" + showLng.y.ToString();
		regionText.text = mapInfo[showLng].GetArea().sName;
		buildingText.text = mapInfo[showLng].building ? mapInfo[showLng].building.sName : "---";
	}

	List<Vector2> traversal = new List<Vector2>();
	List<Vector2> area = new List<Vector2>();
	List<Vector2> traversing = new List<Vector2>();
	Area lastComplete = null;
	Area worstComplete = null;

	private void ReasignArea(Area valueArea , Area newArea) {
		if (valueArea == newArea) {
			return;
		}

		newArea.AddArea(valueArea);
		areas.Remove(valueArea);
	}

	private void DFSWBlock(Vector2 x, Vector2 y) {		
		traversal = new List<Vector2>();

		for (int py = (int)y.x; py < y.y; py++) {
			for (int px = (int)x.x; px < x.y; px++) {
				Vector2 nowP = new Vector2(px, py);
								
				if (!IsTraversal(nowP)) {
					lastComplete = null;
					worstComplete = null;
					area = new List<Vector2>();
					traversing = new List<Vector2>();
					
					AddDFS(x, y, nowP);

					if (sizeOptimize) {
						//正常路徑
						if (area.Count < minOptimizeNum ) {
							//刷新過小區域		
							if (lastComplete != null) {
								AassignArea(area, lastComplete);

							} else if (worstComplete != null) {
								AassignArea(area, worstComplete);

							} else {
								AreaAassignFuc(nowP);
							}
							
						} else {
							AreaAassignFuc(nowP);
						}
					} else {
						AreaAassignFuc(nowP);
					}
				} 
			}
		}
	}

	//區域寫入函式
	private void AreaAassignFuc(Vector2 valueVector) {
		if (areaMode) {
			AassignArea(area, Database.GenBiome(area.Count));
		} else {
			AassignArea(area, Database.biomeBase[mapInfo[valueVector].GetCell()]);
		}
	}
	
	private Area AassignArea(List<Vector2> valueArea, Biome biome) {
		Area newArea = new Area(biome);
		return AassignArea(valueArea, newArea);
	}

	private Area AassignArea(List<Vector2> valueArea , Area assignArea) {
		foreach (Vector2 p in valueArea) {
			assignArea.blocks.Add(mapInfo[p]);
			mapInfo[p].SetArea(assignArea);
		}

		areas.Add(assignArea);
		return assignArea;
	}

	//搜尋鄰近DFS點
	private void NextDFS(Vector2 x, Vector2 y, Vector2 p) {
		Vector2 bp = p + Vector2.right;
		DFSSequence(x, y, p, bp);

		bp = p + Vector2.down;
		DFSSequence(x, y, p, bp);

		bp = p + Vector2.left;
		DFSSequence(x, y, p, bp);

		bp = p + Vector2.up;
		DFSSequence(x, y, p, bp);
	}

	//DFS規則
	private void DFSSequence(Vector2 x, Vector2 y, Vector2 ap, Vector2 bp) {
		if (mapInfo.ContainsKey(bp)) {			
			if (lastComplete == null && mapInfo[bp].GetArea() != null) {				
				if (mapInfo[bp].GetArea().blocks.Count < maxOptimizeNum) {
					lastComplete = mapInfo[bp].GetArea();
				}
			}

			if (worstComplete == null && mapInfo[bp].GetArea() != null) {
				worstComplete = mapInfo[bp].GetArea();
			}

			//在區域內
			if (!IsTraversing(bp)) {
				if (IsSameArea(ap, bp)) {
					if (!blocks[GetBlocksLng(bp)]) {
						AddDFS(x, y, bp);
					}
				}
			}
		}
	}

	//添加DFS點
	private void AddDFS(Vector2 x, Vector2 y, Vector2 p) {
		area.Add(p);
		traversal.Add(p);
		traversing.Add(p);
		NextDFS(x, y, p);
	}

	//DFS規則函式
	private bool IsInBox(Vector2 x, Vector2 y, Vector2 p) {
		return p.x >= x.x && p.x < x.y && p.y >= y.x && p.y < y.y;
	}

	private bool IsTraversal(Vector2 p) {
		return traversal.Contains(p);
	}

	private bool IsTraversing(Vector2 p) {
		return traversing.Contains(p);
	}

	private bool IsSameArea(Vector2 ap, Vector2 bp) {
		if (mapInfo[ap].GetCell() == mapInfo[bp].GetCell()) {
			return true;
		}
		return false;
	}

}
