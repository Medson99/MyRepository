using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckersBoard : MonoBehaviour
{
	public Piece[,] pieces = new Piece[8, 8];
	public GameObject whitePiecePrefab;
	public GameObject blacklPiecePrefab;

	private Vector3 boardOffset = new Vector3(-4.0f, 0, -4.0f);
	private Vector3 pieceOffset = new Vector3(0.5f, 0, 0.5f);

	private Piece selectedPiece;

	private Vector2 mouseOver;
	private Vector2 startDrag;
	private Vector2 endDrag;

	private void Start()
	{
		GenerateBoard();
	}
	void Update()
	{
		UpdateMouseOver();
		//Debug.Log(mouseOver);

		var x = (int) mouseOver.x;
		var y = (int) mouseOver.y;

		if (Input.GetMouseButtonDown(0))
		{
			SelectPiece(x, y);
		}

		if (Input.GetMouseButtonUp(0))
		{
			TryMove((int) startDrag.x, (int) startDrag.y, x, y);
		}
	}
	/// <summary>
	/// Отслеживание миши
	/// </summary>
	void UpdateMouseOver()
	{
		if (!Camera.main)
		{
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
		{
			mouseOver.x = (int) (hit.point.x - boardOffset.x);
			mouseOver.y = (int) (hit.point.z - boardOffset.z);
		}
		else
		{
			mouseOver.x = -1;
			mouseOver.y = -1;
		}
	}
	/// <summary>
	/// Выбор шашки
	/// </summary>
	private void SelectPiece(int x, int y)
	{
		// За пределами поля
		if (x < 0 || x >= pieces.Length || y < 0 || y >= pieces.Length)
		{
			return;
		}


		Piece p = pieces[x, y];
		if (p != null)
		{
			selectedPiece = p;
			startDrag = mouseOver;
			
		}
	}

	private void TryMove(int x1, int y1, int x2, int y2)
	{
		startDrag = new Vector2(x1, y1);
		endDrag = new Vector2(x2, y2);
		selectedPiece = pieces[x1, y1];

		MovePiece(selectedPiece, x2, y2);
	}
	/// <summary>
	/// Генерация доски
	/// </summary>
	private void GenerateBoard()
	{
		//Генерируем белую команду
		for (int y = 0; y < 3; y++)
		{
			bool oddRow = (y % 2 == 0);
			for (int x = 0; x < 8; x += 2)
			{
				GeneratePiece((oddRow) ? x : x + 1, y);
			}
		}

		//Генерируем чёрную команду
		for (int y = 7; y > 4; y--)
		{
			bool oddRow = (y % 2 == 0);
			for (int x = 0; x < 8; x += 2)
			{
				GeneratePiece((oddRow) ? x : x + 1, y);
			}
		}
	}
	/// <summary>
	/// Генерация шашек
	/// </summary>
	private void GeneratePiece(int x, int y)
	{
		bool isWhiteTeam = isWhiteTeam = (y > 3) ? false : true;
		GameObject go = Instantiate(isWhiteTeam ? whitePiecePrefab : blacklPiecePrefab) as GameObject;
		go.transform.SetParent(transform);
		Piece p = go.GetComponent<Piece>();
		pieces[x, y] = p;
		MovePiece(p, x, y);
	}
	/// <summary>
	/// Перемещение шашки
	/// </summary>
	private void MovePiece(Piece p, int x, int y)
	{
		p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + boardOffset + pieceOffset;
	}
}
