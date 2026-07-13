# \# 🎮 Mini Survivor

# 

# Unity 2D 기반의 Vampire Survivors 스타일 생존 게임입니다.

# 

# 플레이어는 자동으로 적을 공격하며 경험치를 획득하고 레벨업을 통해 원하는 능력을 선택하여 더욱 강해질 수 있습니다.

# 

# \---

# 

# \## 🎥 Gameplay

# 

# > GIF 또는 플레이 영상 첨부

# 

# \---

# 

# \## 🕹️ Features

# 

# \### Player

# \- WASD 이동

# \- 자동 공격

# \- 체력 시스템

# \- 피격 무적 시간

# \- 피격 이펙트

# 

# \### Enemy

# \- 플레이어 추적 AI

# \- 체력 시스템

# \- 피격 이펙트

# \- 사망 이펙트

# \- 경험치 드롭

# 

# \### Level System

# \- 경험치 획득

# \- 레벨업

# \- 능력 선택 UI

# \- 능력치 강화

# 

# \### Skill

# \- 공격력 증가

# \- 공격 속도 증가

# \- 이동 속도 증가

# \- 최대 체력 증가

# 

# \### UI

# \- HUD

# \- 경험치 바

# \- 레벨 표시

# \- 킬 수

# \- 생존 시간

# \- 게임오버 화면

# 

# \### Sound

# \- 공격

# \- 피격

# \- 적 사망

# \- 레벨업

# \- 버튼 클릭

# 

# \---

# 

# \# ⚙️ Tech Stack

# 

# \- Unity 6

# \- C#

# \- ScriptableObject

# \- Object Pooling

# \- TextMeshPro

# \- Unity Input System

# 

# \---

# 

# \# 📂 Project Structure

# 

# Assets

# ```

# Scripts

# │

# ├── Player

# │ ├── PlayerController

# │ ├── PlayerAttack

# │ ├── PlayerHealth

# │ └── PlayerLevel

# │

# ├── Enemy

# │ ├── EnemyAI

# │ └── EnemyHealth

# │

# ├── Projectile

# │ └── Projectile

# │

# ├── UI

# │ ├── HUDUI

# │ └── LevelUpUI

# │

# ├── Managers

# │ ├── GameManager

# │ ├── AudioManager

# │ └── PoolManager

# │

# └── Data

# └── SkillData

