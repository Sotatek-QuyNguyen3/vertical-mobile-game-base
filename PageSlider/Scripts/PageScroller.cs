#region Includes
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace TS.PageSlider
{
    /// <summary>
    /// The PageScroller class manages scrolling within a PageSlider component. 
    /// It handles user interaction for swiping between pages and snapping to the closest page on release.
    /// </summary>
    public class PageScroller : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        #region Variables

        [Header("Configuration")]

        private int _pageCount;

        /// <summary>
        /// Minimum delta drag required to consider a page change (normalized value between 0 and 1).
        /// </summary>
        [Tooltip("Minimum delta drag required to consider a page change (normalized value between 0 and 1)")]
        [SerializeField] private float _minDeltaDrag = 0.1f;

        /// <summary>
        /// Duration (in seconds) for the page snapping animation.
        /// </summary>
        [Tooltip("Duration (in seconds) for the page snapping animation")]
        [SerializeField] private float _snapDuration = 0.3f;

        [Header("Events")]

        /// <summary>
        /// Event triggered when a page change starts. 
        /// The event arguments are the index of the current page and the index of the target page.
        /// </summary>
        [Tooltip("Event triggered when a page change starts: index current page, index of target page")]
        public UnityEvent<int, int> OnPageChangeStarted;

        /// <summary>
        /// Event triggered when a page change ends. 
        /// The event arguments are the index of the current page and the index of the new active page.
        /// </summary>
        [Tooltip("Event triggered when a page change ends: index of the current page, index of the new active page")]
        public UnityEvent<int, int> OnPageChangeEnded;

        /// <summary>
        /// Gets the rectangle of the ScrollRect component used for scrolling.
        /// </summary>
        public Rect Rect
        {
            get
            {
#if UNITY_EDITOR
                if (_scrollRect == null)
                {
                    _scrollRect = FindScrollRect();
                }
#endif
                return ((RectTransform)_scrollRect.transform).rect;
            }
        }

        /// <summary>
        /// Gets the RectTransform of the content being scrolled within the ScrollRect.
        /// </summary>
        public RectTransform Content
        {
            get
            {
#if UNITY_EDITOR
                if (_scrollRect == null)
                {
                    _scrollRect = FindScrollRect();
                }
#endif
                return _scrollRect.content;
            }
        }

        private ScrollRect _scrollRect;

        private int _currentPage; // Index of the currently active page.
        private int _targetPage; // Index of the target page during a page change animation.

        private float _startNormalizedPosition; // Normalized position of the scroll bar when drag begins.
        private float _targetNormalizedPosition; // Normalized position of the scroll bar for the target page.
        private float _moveSpeed; // Speed of the scroll bar animation (normalized units per second).
        private float _initialSpeed; // initial speed when starting movement
        private float _elapsedTime = 0f; // time elapsed since the start of movement

        #endregion

        private void Awake()
        {
            _scrollRect = FindScrollRect();
        }

        private void Update()
        {
            // If there is no movement, exit the function.
            if (_moveSpeed == 0) { return; }

            // Update the elapsed time
            _elapsedTime += Time.deltaTime;

            // Calculate the current speed based on time and initial speed
            float timeRatio = Mathf.Clamp01(_elapsedTime / _snapDuration); // time ratio (clamped between 0 and 1)

            // Adjust timeRatio based on the sign of the initial speed
            float adjustedTimeRatio = _initialSpeed < 0 ? -timeRatio : timeRatio;

            // Scale the speed towards zero, maintaining the direction
            _moveSpeed = _initialSpeed * (1 - adjustedTimeRatio);

            // Get the current position of the scroll rect and update it according to moveSpeed and deltaTime
            var position = _scrollRect.horizontalNormalizedPosition;
            position += _moveSpeed * Time.deltaTime;

            // Determine the min and max limits of the position
            var min = _moveSpeed > 0 ? position : _targetNormalizedPosition;
            var max = _moveSpeed > 0 ? _targetNormalizedPosition : position;
            position = Mathf.Clamp(position, min, max);

            // Update the position of the scroll rect
            _scrollRect.horizontalNormalizedPosition = position;

            // Check if the position has reached the target
            if (Mathf.Abs(_targetNormalizedPosition - position) < Mathf.Epsilon || _moveSpeed <= 0.0001f || _moveSpeed >= -0.0001f)
            {
                // Stop movement
                _moveSpeed = 0;
                _elapsedTime = 0; // Reset the elapsed time
                _initialSpeed = 0; // Reset the initial speed

                // Trigger the OnPageChangeEnded event to notify that the movement is complete
                OnPageChangeEnded?.Invoke(_currentPage, _targetPage);

                // Update _currentPage to the current page
                _currentPage = _targetPage;
            }
        }

        // Function to initialize the initial movement speed
        public void StartPageChange(float initialSpeed, float decelerationTime, int targetPage)
        {
            _initialSpeed = initialSpeed;
            _moveSpeed = initialSpeed;
            _snapDuration = decelerationTime;
            _targetPage = targetPage;

            // Set the target position of the page based on the index of the target page
            // _targetNormalizedPosition = GetTargetPagePosition(targetPage); // You can define this function further
            _elapsedTime = 0; // Reset the elapsed time to 0 to restart the deceleration process
        }

        public void SetPageCount(int count){
            _pageCount = count;
        }

        public void SetPage(int index)
        {
            _scrollRect.horizontalNormalizedPosition = GetTargetPagePosition(index);
            _targetPage = index;
            _currentPage = index;
            OnPageChangeEnded?.Invoke(0, _currentPage);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Store the starting normalized position of the scroll bar.
            _startNormalizedPosition = _scrollRect.horizontalNormalizedPosition;

            // Check if the target page is different from the current page.
            if (_targetPage != _currentPage)
            {
                // If they are different, it means we were potentially in the middle of an animation
                // for a previous page change that got interrupted by this drag. 
                // Therefore, signal the end of the previous page change animation (if any)
                // by invoking the OnPageChangeEnded event.
                // The event arguments are the index of the previous page (_currentPage) 
                // and the index of the target page (_targetPage).
                OnPageChangeEnded?.Invoke(_currentPage, _targetPage);

                // Update the _currentPage variable to reflect the target page,
                // as this is now the intended page after the drag begins.
                _currentPage = _targetPage;
            }

            // Reset the move speed to 0 to stop any ongoing scroll animations.
            // This is necessary because a drag interaction might interrupt an ongoing page change animation.
            _moveSpeed = 0;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            // Calculate the width of a single page (normalized value between 0 and 1).
            // This is achieved by dividing 1 by the total number of pages.
            var pageWidth = 1f / GetPageCount();

            // Calculate the normalized position of the current page.
            // When snapping to a page, this position should ideally match the starting normalized position.
            var pagePosition = _currentPage * pageWidth;

            // Get the current normalized position of the scroll rect.
            var currentPosition = _scrollRect.horizontalNormalizedPosition;

            // Determine the minimum amount of drag required (normalized value) to consider a page change.
            // This is calculated by multiplying the page width by the _minDeltaDrag value.
            var minPageDrag = pageWidth * _minDeltaDrag;

            // Check if the drag direction is forward or backward.
            // This is determined by comparing the current position with the starting position.
            // A higher current position indicates a forward drag.
            var isForwardDrag = _scrollRect.horizontalNormalizedPosition > _startNormalizedPosition;

            // Calculate the normalized position where a page change should occur (switchPageBreakpoint).
            // This is calculated by adding (for forward drag) or subtracting (for backward drag) 
            // the minimum page drag distance from the current page position.
            var switchPageBreakpoint = pagePosition + (isForwardDrag ? minPageDrag : -minPageDrag);

            // Determine if a page change should occur based on the current position and the switchPageBreakpoint.
            // If it's a forward drag and the current position is greater than the switchPageBreakpoint, 
            // it means the user has dragged enough to switch to the next page.
            // Similarly, for a backward drag, if the current position is less than the switchPageBreakpoint, 
            // a page change to the previous page is triggered.
            var page = _currentPage;
            if (isForwardDrag && currentPosition > switchPageBreakpoint)
            {
                page++;
            }
            else if (!isForwardDrag && currentPosition < switchPageBreakpoint)
            {
                page--;
            }

            // Call the ScrollToPage function to initiate the page change animation for the determined page.
            MenuManager.Instance?.OnPageChangeEnded(0,page);
            ScrollToPage(page);
        }

        /// <summary>
        /// This function handles initiating a page change animation based on a target page index 
        /// during a scroll interaction. It calculates the target scroll position, determines if a page change 
        /// is required based on drag distance and direction, and triggers the animation if necessary.
        /// </summary>
        /// <param name="page">The index of the target page to scroll to.</param>
        public void ScrollToPage(int page)
        {
            // Calculate the target normalized position for the scroll rect based on the target page index.
            _targetNormalizedPosition = GetTargetPagePosition(page);

            // Calculate the speed required to reach the target position within the snap duration.
            _moveSpeed = (_targetNormalizedPosition - _scrollRect.horizontalNormalizedPosition) / _snapDuration;
            StartPageChange(_moveSpeed, _snapDuration, page);

            // Update the target page variable to reflect the new target page.
            _targetPage = page;

            // If the target page is different from the current page, 
            // invoke the OnPageChangeStarted event to signal the beginning of the page change animation.
            if (_targetPage != _currentPage)
            {
                OnPageChangeStarted?.Invoke(_currentPage, _targetPage);
            }
        }

        /// <summary>
        /// Calculates the number of scrollable pages in the scroll view, considering the content and viewport width.
        /// </summary>
        /// <returns>The number of scrollable pages.</returns>
        private int GetPageCount()
        {
            // var contentWidth = _scrollRect.content.rect.width;
            // var rectWidth = ((RectTransform)_scrollRect.transform).rect.size.x;
            // return Mathf.RoundToInt(contentWidth / rectWidth) - 1;
            return _pageCount - 1;
        }

        private float GetTargetPagePosition(int page)
        {
            return page * (1f / GetPageCount());
        }

        private ScrollRect FindScrollRect()
        {
            var scrollRect = GetComponentInChildren<ScrollRect>();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (scrollRect == null)
            {
                Debug.LogError("Missing ScrollRect in Children");
            }
#endif
            return scrollRect;
        }
    }
}