[System.Serializable]
//excel 파일을 가져오기 위한 별도의 클래스
public class DialogDBEntity
{
	//excel 파일의 행이름과 순서 반드시 일치하도록 설정
	public int index;
	public string types;
	public string name;
	public string comment;
	public bool dialog_flag;
	public int index_back;
}