﻿// Modal/ModalManager.cs

using UnityEngine;

namespace Modal {
    public class ModalManager : MonoBehaviour {

        private static ModalManager manager;
        private GameObject existingInstance;
        public GameObject prefab;

        public ModalManager ()
        {
            ModalManager.manager = this;
        }

        public static ModalManager GetInstance ()
        {
            return manager;
        }


        // ModalUI.ButtonResponse is a delegate, with type void and signiture ().
        public void Show (string title, string okButtonTitle, string cancelButtonTitle, ModalUI.ButtonResponse okButton, ModalUI.ButtonResponse cancelButton)
        {
            Hide();
            existingInstance = Instantiate(prefab);

            existingInstance.GetComponent<ModalUI>()
                .SetTitleText(title)
                .SetOKButtonText(okButtonTitle)
                .SetOKButtonResponse(okButton)
                .SetCancelButtonText(cancelButtonTitle)
                .SetCancelButtonResponse(cancelButton)
                .Show();
        }

        public void Show (string title, string okButtonTitle, ModalUI.ButtonResponse okButton)
        {
            Show(title, okButtonTitle, "", okButton, null);
        }

        public void Show (string title)
        {
            Show(title, "", "", null, null);
        }

        public void Hide ()
        {
            if (existingInstance) {
                Destroy(existingInstance);
            }
        }
    }
}