using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class JoystickController : MonoBehaviour,IDragHandler, IPointerUpHandler, IPointerDownHandler {
    
	private GameManager gameManager;
	
	private Image joystickHolder;
    private Image joystickKnob;
    
    public Vector3 inputDirection;
	private Vector3 ballDirection;
    public Rigidbody ball;
	public float ballMovementSpeed = 10f;
    void Start(){
        
		gameManager = FindObjectOfType<GameManager>();
        joystickHolder = GetComponent<Image>();
        joystickKnob = transform.GetChild(0).GetComponent<Image>();
      
    }
    
    public void OnDrag(PointerEventData ped){
        Vector2 position;
		if(gameManager.gameStart == true){
			
			if( RectTransformUtility.ScreenPointToLocalPointInRectangle
					(joystickHolder.rectTransform, 
					ped.position,
					ped.pressEventCamera,
					out position)){
				
					float x = (joystickHolder.rectTransform.pivot.x == 1f) ? position.x *2 + 1 : position.x *2 - 1;
					float y = (joystickHolder.rectTransform.pivot.y == 1f) ? position.y *2 + 1 : position.y *2 - 1;
					
					inputDirection = new Vector3 (x,y,0);
					
					inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;
					ballDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
					joystickKnob.rectTransform.anchoredPosition = new Vector3 (inputDirection.x * (joystickHolder.rectTransform.sizeDelta.x/3)
																		   ,inputDirection.y * (joystickHolder.rectTransform.sizeDelta.y)/3);
					
					}
					
					ball.AddForce(ballDirection * ballMovementSpeed);
		}
    }
    
    public void OnPointerDown(PointerEventData ped){
        
        OnDrag(ped);
		
    }
    
    public void OnPointerUp(PointerEventData ped){
        
        inputDirection = Vector3.zero;
        joystickKnob.rectTransform.anchoredPosition = Vector3.zero;
		ballDirection = Vector3.zero;
		ball.AddForce(ballDirection * ballMovementSpeed);
    }
}