using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image dashCooldownBar;
    [SerializeField] private Image projectileCooldownBar;
    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.maxValue = States.maxHealth;
        States.currentHealth = States.maxHealth;
    }

    void Update()
    {
        //pauseMenu.SetActive(States.isPaused);
        //healthBar.SetActive(true); // Assuming health bar is always visible
        //dashCooldownBar.SetActive(States.dashInCooldown > 0f);
        dashCooldownBar.fillAmount = States.dashInCooldown;
        projectileCooldownBar.fillAmount = States.projectileCooldown;
        healthSlider.value = States.currentHealth;
    }
}