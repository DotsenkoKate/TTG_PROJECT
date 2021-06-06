from pydantic import BaseModel

class User(BaseModel):
    cat_id: int
    login: str
    password: str
    name: str   