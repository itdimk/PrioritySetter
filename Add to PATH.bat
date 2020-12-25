If exist %cd%\set-priority.exe (
	setx path "%path%;%cd%"
	TIMEOUT 3
) Else (
    echo Can't find set-priority.exe. Operation Cancelled.
    PAUSE
)